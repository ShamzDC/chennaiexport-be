
using CHEExportsDataObjects;
using CHEExportsProto;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CHEExportsDataAccessLayer
{
    [Serializable]
    [DataContract]
    public class DALExportInvoice : DALBase
    {
        [DataMember]
        public ExportInvoice iExportInvoice { get; private set; }

        public DALExportInvoice(ExportInvoice aExportInvoice) : base(aExportInvoice)
        {
            iExportInvoice = aExportInvoice;
        }

        public void SetDescriptionAndChildDetails()
        {
            if (iExportInvoice.export_consignee_id > 0)
            {
                iExportInvoice.iExportConsignee = CommonDAL.SelectDataFromDataBase<ExportConsignee>(new string[] { "EXPORT_CONSIGNEE_ID" }, new string[] { "=" },
                    new object[] { iExportInvoice.export_consignee_id }).FirstOrDefault();
            }
            if (iExportInvoice.party_id > 0)
            {
                iExportInvoice.iParty = CommonDAL.SelectDataFromDataBase<Party>(new string[] { "PARTY_ID" }, new string[] { "=" },
           new object[] { iExportInvoice.party_id }).FirstOrDefault();
            }
            if (iExportInvoice.exporter_id > 0)
            {
                iExportInvoice.iExporter = CommonDAL.SelectDataFromDataBase<Exporter>(new string[] { "EXPORTER_ID" }, new string[] { "=" },
           new object[] { iExportInvoice.exporter_id }).FirstOrDefault();
            }
            string config_ids = Constants.Application.discharge_port_id + "," + Constants.Application.loading_port_id + "," + Constants.Application.repaking_status_id + "," + Constants.Application.Region_id;
            List<SubConfig> lstSubConfig = CommonDAL.GetAllSubConfigValueByConfigID(config_ids);
            iExportInvoice.discharge_port_description = lstSubConfig.Where(x => x.s_config_value == iExportInvoice.discharge_port_value).Select(x => x.s_config_description).FirstOrDefault();
            iExportInvoice.loading_port_description = lstSubConfig.Where(x => x.s_config_value == iExportInvoice.loading_port_value).Select(x => x.s_config_description).FirstOrDefault();
            iExportInvoice.status_description = lstSubConfig.Where(x => x.s_config_value == iExportInvoice.status_value).Select(x => x.s_config_description).FirstOrDefault();
            iExportInvoice.region_description = lstSubConfig.Where(x => x.s_config_value == iExportInvoice.region_value).Select(x => x.s_config_description).FirstOrDefault();
        }
        public void CreateNewExportInvoice()
        {
            try
            {
                iExportInvoice.status_value = Constants.Application.Pending_Approved_status;
                SetDescriptionAndChildDetails();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }


        public void SaveExportInvoice(string token)
        {
            try
            {
                ValidateExportInvoiceSave();
                if (iExportInvoice != null && (iExportInvoice.errorMsg_lsit == null || iExportInvoice.errorMsg_lsit.Count == 0))
                {
                    if (iExportInvoice.export_invoice_id == 0)
                    {
                        iExportInvoice.changed_date = DateTime.Now;
                        iExportInvoice.entered_date = DateTime.Now;
                        iExportInvoice.entered_by = iExportInvoice.iLoggedInUserDetails.user_login_id;
                        iExportInvoice.changed_by = iExportInvoice.iLoggedInUserDetails.user_login_id;
                        Save(token);
                        SaveExportInvoiceDetails(token);

                    }
                    else
                    {
                        iExportInvoice.changed_by = iExportInvoice.iLoggedInUserDetails.user_login_id;
                        iExportInvoice.changed_date = DateTime.Now;
                        Update(token);
                        SaveExportInvoiceDetails(token);
                    }
                    SetDescriptionAndChildDetails();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        private void UpdateFinalPaking(ExportInvoiceDetail aExportInvoiceDetail, string token)
        {

            FinalPackingDetail lFinalPackingDetail = new FinalPackingDetail();
            lFinalPackingDetail.final_packing_detail_id = aExportInvoiceDetail.final_packing_detail_id;
            DALFinalPackingDetail lDALFinalPackingDetail = new DALFinalPackingDetail(lFinalPackingDetail);
            lDALFinalPackingDetail.Open(token);
            lFinalPackingDetail.status_value = Constants.Application.OutStock;
            lFinalPackingDetail.changed_date = DateTime.Now;
            lFinalPackingDetail.changed_by = iExportInvoice.iLoggedInUserDetails.user_login_id;
            lDALFinalPackingDetail.Update(token);
        }

        private void ValidateExportInvoiceSave()
        {
            iExportInvoice.errorMsg_lsit = new List<string>();
            var duplicates = iExportInvoice.lstExportInvoiceDetail.GroupBy(x => x.package_no).Where(g => g.Count() > 1).Select(g => g.Key);
            if (duplicates.Any())
            {
                iExportInvoice.errorMsg_lsit.Add("Export invoice item already exists in list");
            }
            if (iExportInvoice.lstExportInvoiceDetail != null && iExportInvoice.lstExportInvoiceDetail.Count > 0)
            {
                foreach (ExportInvoiceDetail lExportInvoiceDetail in iExportInvoice.lstExportInvoiceDetail)
                {
                    if (lExportInvoiceDetail.final_packing_detail_id > 0)
                    {
                        FinalPackingDetail lFinalPackingDetail = CommonDAL.SelectDataFromDataBase<FinalPackingDetail>(new string[] { "FINAL_PACKING_DETAIL_ID" }, new string[] { "=" },
                     new object[] { lExportInvoiceDetail.final_packing_detail_id }).FirstOrDefault();

                        if (lFinalPackingDetail.order_id > 0)
                        {
                            InvoiceDetails lInvoiceDetails = CommonDAL.SelectDataFromDataBase<InvoiceDetails>(new string[] { "ORDER_DETAIL_ID" }, new string[] { "=" },
                           new object[] { lFinalPackingDetail.order_id }).FirstOrDefault();

                            if (lFinalPackingDetail == null || lFinalPackingDetail.order_id == 0)
                            {
                                iExportInvoice.errorMsg_lsit.Add("Invoice is still pending for this pakage no " + lExportInvoiceDetail.package_no + ".");
                            }
                        }
                    }
                }
            }
        }

        private void SaveExportInvoiceDetails(string token)
        {
            if (iExportInvoice.lstExportInvoiceDetail != null && iExportInvoice.lstExportInvoiceDetail.Count > 0)
            {
                foreach (ExportInvoiceDetail lExportInvoiceDetail in iExportInvoice.lstExportInvoiceDetail)
                {
                    if (lExportInvoiceDetail.export_invoice_detail_id == 0)
                    {
                        lExportInvoiceDetail.changed_date = DateTime.Now;
                        lExportInvoiceDetail.entered_date = DateTime.Now;
                        lExportInvoiceDetail.entered_by = iExportInvoice.iLoggedInUserDetails.user_login_id;
                        lExportInvoiceDetail.changed_by = iExportInvoice.iLoggedInUserDetails.user_login_id;
                        lExportInvoiceDetail.export_invoice_id = iExportInvoice.export_invoice_id;
                        DALExportInvoiceDetail lDALExportInvoiceDetail = new DALExportInvoiceDetail(lExportInvoiceDetail);
                        lDALExportInvoiceDetail.SaveExportInvoiceDetail(token);
                        if (iExportInvoice.status_value == Constants.Application.Approved_status)
                        {
                            UpdateFinalPaking(lExportInvoiceDetail, token);
                        }
                    }
                    else
                    {
                        lExportInvoiceDetail.changed_date = DateTime.Now;
                        lExportInvoiceDetail.changed_by = iExportInvoice.iLoggedInUserDetails.user_login_id;
                        DALExportInvoiceDetail lDALExportInvoiceDetail = new DALExportInvoiceDetail(lExportInvoiceDetail);
                        lDALExportInvoiceDetail.UpdateExportInvoiceDetail(token);
                        if (iExportInvoice.status_value == Constants.Application.Approved_status)
                        {
                            UpdateFinalPaking(lExportInvoiceDetail, token);
                        }
                    }
                }
            }
        }
        public void UpdateExportInvoice(string token)
        {
            try
            {
                //ValidateExportInvoiceSave();
                if (iExportInvoice != null && (iExportInvoice.errorMsg_lsit == null || iExportInvoice.errorMsg_lsit.Count == 0))
                {
                    Update(token);
                    //if(iExportInvoice.status_value == "APPRD" && iExportInvoice.order_id != 0)
                    //{
                    //    OrderDetails lOrderDetails = new OrderDetails();
                    //    lOrderDetails.order_detail_id = iExportInvoice.order_id;
                    //    DALOrderDetails lDALOrderDetails = new DALOrderDetails(lOrderDetails);
                    //    lDALOrderDetails.Open(token);
                    //    lOrderDetails.status_value = "COMTE";
                    //    lDALOrderDetails.Save(token);
                    //}
                    SetDescriptionAndChildDetails();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public void DeleteExportInvoice(string token)
        {
            try
            {
                //ValidateExportInvoiceDelete();
                if (iExportInvoice != null && (iExportInvoice.errorMsg_lsit == null || iExportInvoice.errorMsg_lsit.Count == 0) && iExportInvoice.export_invoice_id > 0)
                {
                    Delete(token);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public void OpenExportInvoice(string token)
        {
            try
            {
                if (iExportInvoice != null && (iExportInvoice.errorMsg_lsit == null || iExportInvoice.errorMsg_lsit.Count == 0) && iExportInvoice.export_invoice_id > 0)
                {
                    Open(token);
                    if (iExportInvoice.export_invoice_id > 0)
                    {
                        iExportInvoice.lstExportInvoiceDetail = CommonDAL.SelectDataFromDataBase<ExportInvoiceDetail>(new string[] { "EXPORT_INVOICE_ID" }, new string[] { "=" },
                      new object[] { iExportInvoice.export_invoice_id }).OrderByDescending(x => x.export_invoice_detail_id).ToList();
                        string config_ids = Constants.Application.Unit_type_id + "," + Constants.Application.Active_Iactive_Status_id;
                        List<SubConfig> lstSubConfig = CommonDAL.GetAllSubConfigValueByConfigID(config_ids);
                        foreach (ExportInvoiceDetail lExportInvoiceDetail in iExportInvoice.lstExportInvoiceDetail)
                        {
                            lExportInvoiceDetail.unit_type_description = lstSubConfig.Where(x => x.s_config_value == lExportInvoiceDetail.unit_type_value).Select(x => x.s_config_description).FirstOrDefault();
                        }
                    }
                    SetDescriptionAndChildDetails();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
        private Stylesheet GenerateStylesheet()
        {
            Stylesheet styleSheet = null;

            Fonts fonts = new Fonts(
                new Font( // Index 0 - default
                    new FontSize() { Val = 11 }

                ),
                new Font( // Index 1 - header.
                    new FontSize() { Val = 11 },
                    new Bold(),
                    new Color() { Rgb = "000000" }

                ));

            Fills fills = new Fills(
                    new Fill(new PatternFill() { PatternType = PatternValues.None }), // Index 0 - default
                    new Fill(new PatternFill() { PatternType = PatternValues.None }), // Index 1 - default
                    new Fill(new PatternFill(new ForegroundColor { Rgb = new HexBinaryValue() { Value = "FFFFFF" } })
                    { PatternType = PatternValues.Solid }) // Index 2 - header
                );

            Borders borders = new Borders(
                    new Border(), // index 0 default
                    new Border( // index 1 black border
                        new LeftBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Hair },
                        new RightBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Hair },
                        new TopBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Hair },
                        new BottomBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Hair },
                        new DiagonalBorder())
                );

            CellFormats cellFormats = new CellFormats(
                    new CellFormat(), // default
                    new CellFormat(new Alignment() { WrapText = true }) { FontId = 0, FillId = 0, BorderId = 1, ApplyBorder = true }, // body
                    new CellFormat { FontId = 1, FillId = 2, BorderId = 1, ApplyFill = true } // header

                );



            styleSheet = new Stylesheet(fonts, fills, borders, cellFormats);

            return styleSheet;
        }

        private void SetColumnWidth(WorksheetPart worksheetPart)
        {
            Columns columns = new Columns(
                                     new Column
                                     {
                                         Min = 1,
                                         Max = 25,
                                         Width = 25,
                                         CustomWidth = true
                                     },
                                     new Column
                                     {
                                         Min = 1,
                                         Max = 25,
                                         Width = 25,
                                         CustomWidth = true
                                     }
                                      );
            worksheetPart.Worksheet.AppendChild(columns);
        }
        public byte[] GenerateExportInvoiceExcel(protoStringData2 aprotoStringData2)
        {
            byte[] GeneratedExcelFile = { };
            string basePath = string.Empty;
            string FileName = string.Empty;
            string AttachmentFolder = string.Empty;
            try
            {
                OpenExportInvoice("");
                {
                   SubConfig lSubConfig = CommonDAL.SelectDataFromDataBase<SubConfig>(new string[] { "S_CONFIG_VALUE" }, new string[] { "=" },
            new object[] { "BAPTH" }).FirstOrDefault();
                    FileName = "ExportInvoice" + ".xlsx";
                    AttachmentFolder = Path.Join(lSubConfig.s_config_description, Constants.Application.ExportInvoice);
                    basePath = CommonDAL.GetPlatformFilePath(AttachmentFolder);

                    if (!Directory.Exists(basePath))
                    {
                        Directory.CreateDirectory(basePath);
                    }

                    string FileLoaction = basePath + "\\" + FileName;
                    using (SpreadsheetDocument document = SpreadsheetDocument.Create(FileLoaction, SpreadsheetDocumentType.Workbook))
                    {

                        WorkbookPart workbookPart = document.AddWorkbookPart();
                        workbookPart.Workbook = new Workbook();

                        WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                        worksheetPart.Worksheet = new Worksheet();

                        WorkbookStylesPart stylePart = workbookPart.AddNewPart<WorkbookStylesPart>();
                        stylePart.Stylesheet = GenerateStylesheet();
                        stylePart.Stylesheet.Save();
                        SetColumnWidth(worksheetPart);
                        Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                        Sheet sheet = new Sheet()
                        { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "ExportInvoice" };

                        sheets.Append(sheet);

                        workbookPart.Workbook.Save();

                        SheetData sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());
                        if (iExportInvoice != null)
                        {
                            Row rowselect_0 = new Row();
                            rowselect_0.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect_0.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect_0.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect_0.Append(ConstructCell("INVOICE", CellValues.String, 2));
                            //rowselect_0.Append(ConstructCell((Convert.ToString("")), OpenXmlSpreedSheet.CellValues.String,1));
                            //rowselect_0.Append(ConstructCell((Convert.ToString("")), OpenXmlSpreedSheet.CellValues.String,1));
                            sheetData.Append(rowselect_0);

                            Row rowselect_01 = new Row();
                            sheetData.Append(rowselect_01);

                            Row rowselect1 = new Row();
                            rowselect1.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect1.Append(ConstructCell(Convert.ToString("Exporter"), CellValues.String, 1));
                            rowselect1.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselect1.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect1.Append(ConstructCell(Convert.ToString("CHENNAI"), CellValues.String, 2));
                            rowselect1.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            sheetData.Append(rowselect1);

                            Row rowselec_ = new Row();
                            rowselec_.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselec_.Append(ConstructCell(Convert.ToString(iExportInvoice.iExporter.exporter_name), CellValues.String,2));
                            rowselec_.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselec_.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselec_.Append(ConstructCell(Convert.ToString("EXPORTS"), CellValues.String, 2));
                            rowselec_.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            sheetData.Append(rowselec_);

                            Row rowselect2 = new Row();
                            rowselect2.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect2.Append(ConstructCell(Convert.ToString("Email :" + "" + iExportInvoice.iExporter.email_id), CellValues.String,1));
                            rowselect2.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect2.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect2.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselect2.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            sheetData.Append(rowselect2);

                            Row rowselect3 = new Row();
                            rowselect3.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect3.Append(ConstructCell(Convert.ToString("Phone :" + "" + iExportInvoice.iExporter.contact_no), CellValues.String,1));
                            rowselect3.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect3.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect3.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselect3.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            sheetData.Append(rowselect3);

                            Row rowselect4 = new Row();
                            rowselect4.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect4.Append(ConstructCell(Convert.ToString("GST No :" + "" + iExportInvoice.iExporter.gstn_uin_number), CellValues.String,1));
                            rowselect4.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect4.Append(ConstructCell(Convert.ToString("Invoice No"), CellValues.String, 1));
                            rowselect4.Append(ConstructCell(Convert.ToString(iExportInvoice.invoice_no), CellValues.String, 2));
                            rowselect4.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            sheetData.Append(rowselect4);

                            Row rowselect5 = new Row();
                            rowselect5.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect5.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect5.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect5.Append(ConstructCell(Convert.ToString("Issued Date"), CellValues.String, 1));
                            rowselect5.Append(ConstructCell(Convert.ToString(iExportInvoice.invoice_date), CellValues.String, 2));
                            rowselect5.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            sheetData.Append(rowselect5);

                            Row rowselect6 = new Row();
                            sheetData.Append(rowselect6);

                            Row rowselectt7 = new Row();
                            rowselectt7.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselectt7.Append(ConstructCell(Convert.ToString("Consignee"), CellValues.String, 1));
                            rowselectt7.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselectt7.Append(ConstructCell(string.Format("Notify Party"), CellValues.String, 1));
                            rowselectt7.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselectt7.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            sheetData.Append(rowselectt7);

                            Row rowselectt8 = new Row();
                            rowselectt8.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselectt8.Append(ConstructCell(Convert.ToString(iExportInvoice.iExportConsignee.export_consignee_name), CellValues.String, 2));
                            rowselectt8.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselectt8.Append(ConstructCell(Convert.ToString(iExportInvoice.iParty.party_name), CellValues.String, 2));
                            rowselectt8.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselectt8.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            sheetData.Append(rowselectt8);

                            Row rowselectt9 = new Row();
                            rowselectt9.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselectt9.Append(ConstructCell(Convert.ToString(CommonDAL.ConcatAddressLine(iExportInvoice.iExportConsignee.address_line_1,
                            iExportInvoice.iExportConsignee.address_line_2, iExportInvoice.iExportConsignee.address_line_3)), CellValues.String, 1));
                            rowselectt9.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselectt9.Append(ConstructCell(Convert.ToString(CommonDAL.ConcatAddressLine(iExportInvoice.iParty.address_line_1,
                            iExportInvoice.iParty.address_line_2, iExportInvoice.iParty.address_line_3)), CellValues.String, 1));
                            rowselectt9.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselectt9.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            sheetData.Append(rowselectt9);

                            Row rowselect10 = new Row();
                            rowselect10.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect10.Append(ConstructCell(Convert.ToString("Email :" + "" + iExportInvoice.iExportConsignee.email_id), CellValues.String,1));
                            rowselect10.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect10.Append(ConstructCell(Convert.ToString("Email :" + "" + iExportInvoice.iParty.email_id), CellValues.String, 1));
                            rowselect10.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselect10.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            sheetData.Append(rowselect10);

                            Row rowselect11 = new Row();
                            rowselect11.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect11.Append(ConstructCell(Convert.ToString("Phone :" + "" + iExportInvoice.iExportConsignee.contact_no), CellValues.String,1));
                            rowselect11.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect11.Append(ConstructCell(Convert.ToString("Phone :" + "" + iExportInvoice.iParty.contact_no), CellValues.String, 1));
                            rowselect11.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselect11.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            sheetData.Append(rowselect11);

                            Row rowselect12 = new Row();
                            sheetData.Append(rowselect12);


                            Row rowselect13 = new Row();
                            rowselect13.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect13.Append(ConstructCell(Convert.ToString("Buyer's Order No"), CellValues.String,1));
                            rowselect13.Append(ConstructCell(Convert.ToString(iExportInvoice.buyers_order_no_date), CellValues.String,2));
                            rowselect13.Append(ConstructCell(Convert.ToString("Pre-Carriage by"), CellValues.String, 1));
                            rowselect13.Append(ConstructCell(Convert.ToString(iExportInvoice.pre_carriage_by), CellValues.String, 2));
                            rowselect13.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            sheetData.Append(rowselect13);

                            Row rowselect14 = new Row();
                            rowselect14.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect14.Append(ConstructCell(Convert.ToString("Country of Origin of Goods"), CellValues.String,1));
                            rowselect14.Append(ConstructCell(Convert.ToString(iExportInvoice.country_of_origin_of_goods), CellValues.String, 2));
                            rowselect14.Append(ConstructCell(Convert.ToString("Place of Receipt of pre-Carrier"), CellValues.String, 1));
                            rowselect14.Append(ConstructCell(Convert.ToString(iExportInvoice.place_of_receipt_of_pre_carrier), CellValues.String, 2));
                            rowselect14.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            sheetData.Append(rowselect14);

                            Row rowselect15 = new Row();
                            rowselect15.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect15.Append(ConstructCell(Convert.ToString("Country of Final Destination"), CellValues.String,1));
                            rowselect15.Append(ConstructCell(Convert.ToString(iExportInvoice.country_of_final_destination), CellValues.String, 2));
                            rowselect15.Append(ConstructCell(Convert.ToString("Vessel/Flight No"), CellValues.String, 1));
                            rowselect15.Append(ConstructCell(Convert.ToString(iExportInvoice.vessel_flight_no), CellValues.String, 2));
                            rowselect15.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            sheetData.Append(rowselect15);


                            Row rowselect16 = new Row();
                            rowselect16.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect16.Append(ConstructCell(Convert.ToString("Terms of delivery Payment"), CellValues.String,1));
                            rowselect16.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect16.Append(ConstructCell(Convert.ToString("Port of Loading"), CellValues.String, 1));
                            rowselect16.Append(ConstructCell(Convert.ToString(iExportInvoice.loading_port_description), CellValues.String, 2));
                            rowselect16.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            sheetData.Append(rowselect16);

                            Row rowselect17 = new Row();
                            rowselect17.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect17.Append(ConstructCell(Convert.ToString(iExportInvoice.terms_of_delivery_payment), CellValues.String, 2));
                            rowselect17.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect17.Append(ConstructCell(Convert.ToString("Port of Discharge"), CellValues.String, 1));
                            rowselect17.Append(ConstructCell(Convert.ToString(iExportInvoice.discharge_port_description), CellValues.String, 2));
                            rowselect17.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            sheetData.Append(rowselect17);

                            Row rowselect18 = new Row();
                            rowselect18.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect18.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect18.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect18.Append(ConstructCell(Convert.ToString("Final Destination"), CellValues.String, 1));
                            rowselect18.Append(ConstructCell(Convert.ToString(iExportInvoice.country_of_final_destination), CellValues.String, 2));
                            rowselect18.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            sheetData.Append(rowselect18);

                            Row rowselect111 = new Row();
                            sheetData.Append(rowselect111);

                            Row rowselect1111 = new Row();
                            sheetData.Append(rowselect1111);

                            Row rowselect19 = new Row();
                            rowselect19.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect19.Append(ConstructCell(Convert.ToString("S.No"), CellValues.String,2));
                            rowselect19.Append(ConstructCell(Convert.ToString("Item Details|Description"), CellValues.String,2));
                            rowselect19.Append(ConstructCell(Convert.ToString("Pakage no"), CellValues.String,2));
                            rowselect19.Append(ConstructCell(Convert.ToString("Qty"), CellValues.String,2));
                            rowselect19.Append(ConstructCell(Convert.ToString("Unit"), CellValues.String, 2));
                            rowselect19.Append(ConstructCell(Convert.ToString("Price"), CellValues.String, 2));
                            rowselect19.Append(ConstructCell(Convert.ToString("Amount"), CellValues.String, 2));
                            sheetData.Append(rowselect19);
                            int i = 1;
                            foreach (ExportInvoiceDetail lExportInvoiceDetail in iExportInvoice.lstExportInvoiceDetail)
                            {
                                Product lProduct = CommonDAL.SelectDataFromDataBase<Product>(new string[] { "PRODUCT_ID" }, new string[] { "=" },
                                new object[] { lExportInvoiceDetail.product_id }).FirstOrDefault();
                                Row rowselect20 = new Row();
                                rowselect20.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                                rowselect20.Append(ConstructCell(Convert.ToString(i), CellValues.String,1));
                                rowselect20.Append(ConstructCell(Convert.ToString(lProduct.product_name), CellValues.String,1));
                                    rowselect20.Append(ConstructCell(Convert.ToString(lExportInvoiceDetail.package_no), CellValues.String,1));
                                rowselect20.Append(ConstructCell(Convert.ToString(lExportInvoiceDetail.quantity), CellValues.String,1));
                                rowselect20.Append(ConstructCell(Convert.ToString(lExportInvoiceDetail.unit_type_description), CellValues.String, 1));
                                rowselect20.Append(ConstructCell(Convert.ToString(lExportInvoiceDetail.rate), CellValues.String, 1));
                                rowselect20.Append(ConstructCell(Convert.ToString(lExportInvoiceDetail.amount), CellValues.String, 2));
                                sheetData.Append(rowselect20);
                                i++;
                            }
                            Row rowselect21 = new Row();
                            rowselect21.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect21.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect21.Append(ConstructCell(Convert.ToString("No of Pakage"), CellValues.String,1));
                            rowselect21.Append(ConstructCell(Convert.ToString("Net Weight"), CellValues.String,1));
                            rowselect21.Append(ConstructCell(Convert.ToString("Gross Weight"), CellValues.String,1));
                            rowselect21.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect21.Append(ConstructCell(Convert.ToString("Sub Total"), CellValues.String, 2));
                            rowselect21.Append(ConstructCell(Convert.ToString(iExportInvoice.taxable_value), CellValues.String, 1));
                            sheetData.Append(rowselect21);

                            Row rowselect22 = new Row();
                            rowselect22.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect22.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect22.Append(ConstructCell(Convert.ToString(iExportInvoice.no_and_kind_of_pakage), CellValues.String, 2));
                            rowselect22.Append(ConstructCell(Convert.ToString(iExportInvoice.net_weight), CellValues.String,2));
                            rowselect22.Append(ConstructCell(Convert.ToString(iExportInvoice.gross_weight), CellValues.String,2));
                            rowselect22.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect22.Append(ConstructCell(Convert.ToString("Round Off"), CellValues.String, 2));
                            rowselect22.Append(ConstructCell(Convert.ToString(iExportInvoice.round_off), CellValues.String, 1));
                            sheetData.Append(rowselect22);

                            Row rowselect23 = new Row();
                            rowselect23.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect23.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect23.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                         
                            rowselect23.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect23.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect23.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect23.Append(ConstructCell(Convert.ToString("SGST     " + "" + iExportInvoice.sgst_tax_percentage), CellValues.String, 1));
                            rowselect23.Append(ConstructCell(Convert.ToString(iExportInvoice.sgst_value), CellValues.String, 1));
                            sheetData.Append(rowselect23);

                            Row rowselect24 = new Row();
                            rowselect24.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect24.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect24.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect24.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect24.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect24.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect24.Append(ConstructCell(Convert.ToString("CGST     " + "" + iExportInvoice.cgst_tax_percentage), CellValues.String, 1));
                            rowselect24.Append(ConstructCell(Convert.ToString(iExportInvoice.cgst_value), CellValues.String, 1));
                            sheetData.Append(rowselect24);

                            Row rowselect25 = new Row();
                            rowselect25.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect25.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect25.Append(ConstructCell(Convert.ToString("Total in Words"), CellValues.String,1));
                            rowselect25.Append(ConstructCell(Convert.ToString(ConvertAmountToWords(iExportInvoice.invoice_total)), CellValues.String,2));
                            rowselect25.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect25.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect25.Append(ConstructCell(Convert.ToString("Total"), CellValues.String, 2));
                            rowselect25.Append(ConstructCell(Convert.ToString(iExportInvoice.invoice_total), CellValues.String, 2));
                            sheetData.Append(rowselect25);
                            Row rowselec1 = new Row();
                            sheetData.Append(rowselec1);

                            Row rowsele1 = new Row();
                            sheetData.Append(rowsele1);


                            Row rowselect26 = new Row();
                            rowselect26.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect26.Append(ConstructCell(Convert.ToString("Bank Account Details"), CellValues.String,2));
                            rowselect26.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselect26.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect26.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect26.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect26.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            sheetData.Append(rowselect26);

                            Row rowselect27 = new Row();
                            rowselect27.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect27.Append(ConstructCell(Convert.ToString("A/C Name : JJ Traders"), CellValues.String,1));
                            rowselect27.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselect27.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect27.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect27.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect27.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            sheetData.Append(rowselect27);

                            Row rowselect28 = new Row();
                            rowselect28.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect28.Append(ConstructCell(Convert.ToString("Bank : ICICI Bank |A/C No :602305030487"), CellValues.String,1));
                            rowselect28.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselect28.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect28.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect28.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect28.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            sheetData.Append(rowselect28);

                            Row rowselect29 = new Row();
                            rowselect29.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect29.Append(ConstructCell(Convert.ToString("IFCS Code :ICIC0006023"), CellValues.String,1));
                            rowselect29.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselect29.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect29.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect29.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect29.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            sheetData.Append(rowselect29);

                            Row rowselect30 = new Row();
                            rowselect30.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect30.Append(ConstructCell(Convert.ToString("Branch Address :48,Arya gowda road,west mambalam chennai-600033"), CellValues.String,1));
                            rowselect30.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselect30.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect30.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect30.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect30.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            sheetData.Append(rowselect30);

                            Row rowselect31 = new Row();
                            rowselect31.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect31.Append(ConstructCell(Convert.ToString("Branch code : 6032"), CellValues.String,1));
                            rowselect31.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselect31.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect31.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect31.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect31.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            sheetData.Append(rowselect31);

                            Row rowselect32 = new Row();
                            rowselect32.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect32.Append(ConstructCell(Convert.ToString("Swift Code : ICICINBBNRI"), CellValues.String,1));
                            rowselect32.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselect32.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect32.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect32.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect32.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            sheetData.Append(rowselect32);

                            Row rowselect33 = new Row();
                            rowselect33.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect33.Append(ConstructCell(Convert.ToString("AD Code : 6390110"), CellValues.String,1));
                            rowselect33.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselect33.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect33.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            // rowselect33.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect33.Append(ConstructCell(Convert.ToString("JJ Traders"), CellValues.String, 2));
                            sheetData.Append(rowselect33);

                            Row rowselect34 = new Row();
                            rowselect34.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect34.Append(ConstructCell(Convert.ToString("Declaration"), CellValues.String,2));
                            rowselect34.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselect34.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect34.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect34.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect34.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            sheetData.Append(rowselect34);


                            Row rowselect35 = new Row();
                            rowselect35.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect35.Append(ConstructCell(Convert.ToString("We declare that this invoice shows the actual" +
                                " price of the goods described and that all pariticulars are true and correct."), CellValues.String,1));
                            rowselect35.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselect35.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect35.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect35.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect35.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            sheetData.Append(rowselect35);

                            worksheetPart.Worksheet.Save();

                        }
                        GeneratedExcelFile = GenerateExportInvoiceExcel(FileLoaction);
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return GeneratedExcelFile;
        }

        private byte[] GenerateExportInvoiceExcel(string FileLoaction)
        {
            byte[] istrFileContent = { };

            if (!FileLoaction.EndsWith(@"\"))
            {
                FileLoaction += @"\";
            }
           
            if (Directory.Exists(FileLoaction) && !string.IsNullOrWhiteSpace(FileLoaction))
            {
                using (FileStream fileSt = new FileStream(FileLoaction, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader reader = new BinaryReader(fileSt))
                    {
                        istrFileContent = reader.ReadBytes(Convert.ToInt32(fileSt.Length));
                    }
                }
            }
            return istrFileContent;
            // throw new NotImplementedException();
        }

        private static readonly string[] _ones = { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
        private static readonly string[] _tens = { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

        public static string ConvertAmountToWords(decimal amount)
        {
            if (amount == 0)
                return "Zero dollars";

            string dollars = ConvertNumberToWords((int)amount);

            if (dollars == "Zero")
            {
                return $"{dollars}";
            }
            else
            {
                return $"{dollars}";
            }
        }

        private static string ConvertNumberToWords(int number)
        {
            if (number == 0)
                return "Zero";

            if (number < 0)
                return "Minus " + ConvertNumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += ConvertNumberToWords(number / 1000000) + " Million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += ConvertNumberToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += ConvertNumberToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                if (number < 20)
                    words += _ones[number];
                else
                {
                    words += _tens[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + _ones[number % 10];
                }
            }

            return words.Trim();
        }

        private Cell ConstructCell(string value, CellValues dataType,
        uint styleIndex = 0)
        {
            return new Cell()
            {
                CellValue = new CellValue(value),
                DataType = new EnumValue<CellValues>(dataType),
                StyleIndex = styleIndex
            };
        }
        public byte[] GenerateExportInvoiceWithoutPakageNoExcel(protoStringData2 aprotoStringData2)
        {
            byte[] GeneratedExcelFile = { };
            string basePath = string.Empty;
            string FileName = string.Empty; 
            string AttachmentFolder = string.Empty;
            try
            {
                OpenExportInvoice("");

                {
                    FileName = "ExportInvoiceWithoutPakageNo" + ".xlsx";
                    SubConfig lSubConfig = CommonDAL.SelectDataFromDataBase<SubConfig>(new string[] { "S_CONFIG_VALUE" }, new string[] { "=" },
              new object[] { "BAPTH" }).FirstOrDefault();
                    AttachmentFolder = Path.Join(lSubConfig.s_config_description, Constants.Application.ExportInvoice);
                    basePath = CommonDAL.GetPlatformFilePath(AttachmentFolder);
                    if (!Directory.Exists(basePath))
                    {
                        Directory.CreateDirectory(basePath);
                    }

                    string FileLoaction = basePath + "\\" + FileName;
                    using (SpreadsheetDocument document = SpreadsheetDocument.Create(FileLoaction, SpreadsheetDocumentType.Workbook))
                    {

                        WorkbookPart workbookPart = document.AddWorkbookPart();
                        workbookPart.Workbook = new Workbook();

                        WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                        worksheetPart.Worksheet = new Worksheet();

                        WorkbookStylesPart stylePart = workbookPart.AddNewPart<WorkbookStylesPart>();
                        stylePart.Stylesheet = GenerateStylesheet();
                        stylePart.Stylesheet.Save();
                        SetColumnWidth(worksheetPart);
                        Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                        Sheet sheet = new Sheet()
                        { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "ExportInvoice" };

                        sheets.Append(sheet);

                        workbookPart.Workbook.Save();

                        SheetData sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());
                        if (iExportInvoice != null)
                        {
                            Row rowselect_0 = new Row();
                            rowselect_0.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect_0.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect_0.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect_0.Append(ConstructCell("INVOICE", CellValues.String, 2));
                            //rowselect_0.Append(ConstructCell((Convert.ToString("")), OpenXmlSpreedSheet.CellValues.String,1));
                            //rowselect_0.Append(ConstructCell((Convert.ToString("")), OpenXmlSpreedSheet.CellValues.String,1));
                            sheetData.Append(rowselect_0);

                            Row rowselect_01 = new Row();
                            sheetData.Append(rowselect_01);

                            Row rowselect1 = new Row();
                            rowselect1.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect1.Append(ConstructCell(Convert.ToString("Exporter"), CellValues.String, 1));
                            rowselect1.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselect1.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect1.Append(ConstructCell(Convert.ToString("CHENNAI"), CellValues.String, 2));
                            rowselect1.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            sheetData.Append(rowselect1);

                            Row rowselec_ = new Row();
                            rowselec_.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselec_.Append(ConstructCell(Convert.ToString(iExportInvoice.iExporter.exporter_name), CellValues.String, 2));
                            rowselec_.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselec_.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselec_.Append(ConstructCell(Convert.ToString("EXPORTS"), CellValues.String, 2));
                            rowselec_.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            sheetData.Append(rowselec_);

                            Row rowselect2 = new Row();
                            rowselect2.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect2.Append(ConstructCell(Convert.ToString("Email :" + "" + iExportInvoice.iExporter.email_id), CellValues.String, 1));
                            rowselect2.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect2.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect2.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselect2.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            sheetData.Append(rowselect2);

                            Row rowselect3 = new Row();
                            rowselect3.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect3.Append(ConstructCell(Convert.ToString("Phone :" + "" + iExportInvoice.iExporter.contact_no), CellValues.String, 1));
                            rowselect3.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect3.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect3.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselect3.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            sheetData.Append(rowselect3);

                            Row rowselect4 = new Row();
                            rowselect4.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect4.Append(ConstructCell(Convert.ToString("GST No: " + "" + iExportInvoice.iExporter.gstn_uin_number), CellValues.String, 1));
                            rowselect4.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect4.Append(ConstructCell(Convert.ToString("Invoice No"), CellValues.String, 1));
                            rowselect4.Append(ConstructCell(Convert.ToString(iExportInvoice.invoice_no), CellValues.String, 2));
                            rowselect4.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            sheetData.Append(rowselect4);

                            Row rowselect5 = new Row();
                            rowselect5.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect5.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect5.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect5.Append(ConstructCell(Convert.ToString("Issued Date"), CellValues.String, 1));
                            rowselect5.Append(ConstructCell(Convert.ToString(iExportInvoice.invoice_date), CellValues.String, 2));
                            rowselect5.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            sheetData.Append(rowselect5);

                            Row rowselect6 = new Row();
                            sheetData.Append(rowselect6);

                            Row rowselectt7 = new Row();
                            rowselectt7.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselectt7.Append(ConstructCell(Convert.ToString("Consignee"), CellValues.String, 1));
                            rowselectt7.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselectt7.Append(ConstructCell(string.Format("Notify Party"), CellValues.String, 1));
                            rowselectt7.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselectt7.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            sheetData.Append(rowselectt7);

                            Row rowselectt8 = new Row();
                            rowselectt8.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselectt8.Append(ConstructCell(Convert.ToString(iExportInvoice.iExportConsignee.export_consignee_name), CellValues.String, 2));
                            rowselectt8.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselectt8.Append(ConstructCell(Convert.ToString(iExportInvoice.iParty.party_name), CellValues.String, 2));
                            rowselectt8.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselectt8.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            sheetData.Append(rowselectt8);

                            Row rowselectt9 = new Row();
                            rowselectt9.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselectt9.Append(ConstructCell(Convert.ToString(CommonDAL.ConcatAddressLine(iExportInvoice.iExportConsignee.address_line_1,
                            iExportInvoice.iExportConsignee.address_line_2, iExportInvoice.iExportConsignee.address_line_3)), CellValues.String, 1));
                            rowselectt9.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselectt9.Append(ConstructCell(Convert.ToString(CommonDAL.ConcatAddressLine(iExportInvoice.iParty.address_line_1,
                            iExportInvoice.iParty.address_line_2, iExportInvoice.iParty.address_line_3)), CellValues.String, 1));
                            rowselectt9.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselectt9.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            sheetData.Append(rowselectt9);

                            Row rowselect10 = new Row();
                            rowselect10.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect10.Append(ConstructCell(Convert.ToString("Email :" + "" + iExportInvoice.iExportConsignee.email_id), CellValues.String, 1));
                            rowselect10.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect10.Append(ConstructCell(Convert.ToString("Email :" + "" + iExportInvoice.iParty.email_id), CellValues.String, 1));
                            rowselect10.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselect10.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            sheetData.Append(rowselect10);

                            Row rowselect11 = new Row();
                            rowselect11.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect11.Append(ConstructCell(Convert.ToString("Phone :" + "" + iExportInvoice.iExportConsignee.contact_no), CellValues.String, 1));
                            rowselect11.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect11.Append(ConstructCell(Convert.ToString("Phone :" + "" + iExportInvoice.iParty.contact_no), CellValues.String, 1));
                            rowselect11.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselect11.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            sheetData.Append(rowselect11);

                            Row rowselect12 = new Row();
                            sheetData.Append(rowselect12);


                            Row rowselect13 = new Row();
                            rowselect13.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect13.Append(ConstructCell(Convert.ToString("Buyer's Order No"), CellValues.String, 1));
                            rowselect13.Append(ConstructCell(Convert.ToString(iExportInvoice.buyers_order_no_date), CellValues.String, 2));
                            rowselect13.Append(ConstructCell(Convert.ToString("Pre-Carriage by"), CellValues.String, 1));
                            rowselect13.Append(ConstructCell(Convert.ToString(iExportInvoice.pre_carriage_by), CellValues.String, 2));
                            rowselect13.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            sheetData.Append(rowselect13);

                            Row rowselect14 = new Row();
                            rowselect14.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect14.Append(ConstructCell(Convert.ToString("Country of Origin of Goods"), CellValues.String, 1));
                            rowselect14.Append(ConstructCell(Convert.ToString(iExportInvoice.country_of_origin_of_goods), CellValues.String, 2));
                            rowselect14.Append(ConstructCell(Convert.ToString("Place of Receipt of pre-Carrier"), CellValues.String, 1));
                            rowselect14.Append(ConstructCell(Convert.ToString(iExportInvoice.place_of_receipt_of_pre_carrier), CellValues.String, 2));
                            rowselect14.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            sheetData.Append(rowselect14);

                            Row rowselect15 = new Row();
                            rowselect15.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect15.Append(ConstructCell(Convert.ToString("Country of Final Destination"), CellValues.String,1));
                            rowselect15.Append(ConstructCell(Convert.ToString(iExportInvoice.country_of_final_destination), CellValues.String, 2));
                            rowselect15.Append(ConstructCell(Convert.ToString("Vessel/Flight No"), CellValues.String, 1));
                            rowselect15.Append(ConstructCell(Convert.ToString(iExportInvoice.vessel_flight_no), CellValues.String, 2));
                            rowselect15.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            sheetData.Append(rowselect15);


                            Row rowselect16 = new Row();
                            rowselect16.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect16.Append(ConstructCell(Convert.ToString("Terms of delivery Payment"), CellValues.String,1));
                            rowselect16.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect16.Append(ConstructCell(Convert.ToString("Port of Loading"), CellValues.String, 1));
                            rowselect16.Append(ConstructCell(Convert.ToString(iExportInvoice.loading_port_description), CellValues.String, 2));
                            rowselect16.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            sheetData.Append(rowselect16);

                            Row rowselect17 = new Row();
                            rowselect17.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect17.Append(ConstructCell(Convert.ToString(iExportInvoice.terms_of_delivery_payment), CellValues.String, 2));
                            rowselect17.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect17.Append(ConstructCell(Convert.ToString("Port of Discharge"), CellValues.String, 1));
                            rowselect17.Append(ConstructCell(Convert.ToString(iExportInvoice.discharge_port_description), CellValues.String, 2));
                            rowselect17.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            sheetData.Append(rowselect17);

                            Row rowselect18 = new Row();
                            rowselect18.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect18.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect18.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect18.Append(ConstructCell(Convert.ToString("Final Destination"), CellValues.String, 1));
                            rowselect18.Append(ConstructCell(Convert.ToString(iExportInvoice.country_of_final_destination), CellValues.String, 2));
                            rowselect18.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            sheetData.Append(rowselect18);

                            Row rowselect111 = new Row();
                            sheetData.Append(rowselect111);

                            Row rowselect1111 = new Row();
                            sheetData.Append(rowselect1111);

                            Row rowselect19 = new Row();
                            rowselect19.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect19.Append(ConstructCell(Convert.ToString("S.No"), CellValues.String,2));
                            rowselect19.Append(ConstructCell(Convert.ToString("Item Details|Description"), CellValues.String, 2));
                            //rowselect19.Append(ConstructCell(Convert.ToString("Pakage no"), CellValues.String, 2));
                            rowselect19.Append(ConstructCell(Convert.ToString("Qty"), CellValues.String, 2));
                            rowselect19.Append(ConstructCell(Convert.ToString("Unit"), CellValues.String, 2));
                            rowselect19.Append(ConstructCell(Convert.ToString("Price"), CellValues.String, 2));
                            rowselect19.Append(ConstructCell(Convert.ToString("Amount"), CellValues.String, 2));
                            sheetData.Append(rowselect19);
                            int i = 1;
                            foreach (ExportInvoiceDetail lExportInvoiceDetail in iExportInvoice.lstExportInvoiceDetail)
                            {
                                Product lProduct = CommonDAL.SelectDataFromDataBase<Product>(new string[] { "PRODUCT_ID" }, new string[] { "=" },
                                new object[] { lExportInvoiceDetail.product_id }).FirstOrDefault();
                                Row rowselect20 = new Row();
                                rowselect20.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                                rowselect20.Append(ConstructCell(Convert.ToString(i), CellValues.String,1));
                                rowselect20.Append(ConstructCell(Convert.ToString(lProduct.product_name), CellValues.String,1));
                               // rowselect20.Append(ConstructCell(Convert.ToString(lExportInvoiceDetail.package_no), CellValues.String,1));
                                rowselect20.Append(ConstructCell(Convert.ToString(lExportInvoiceDetail.quantity), CellValues.String,1));
                                rowselect20.Append(ConstructCell(Convert.ToString(lExportInvoiceDetail.unit_type_description), CellValues.String, 1));
                                rowselect20.Append(ConstructCell(Convert.ToString(lExportInvoiceDetail.rate), CellValues.String, 1));
                                rowselect20.Append(ConstructCell(Convert.ToString(lExportInvoiceDetail.amount), CellValues.String, 2));
                                sheetData.Append(rowselect20);
                                i++;
                            }
                            Row rowselect21 = new Row();
                            rowselect21.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect21.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect21.Append(ConstructCell(Convert.ToString("No of Pakage"), CellValues.String,1));
                            rowselect21.Append(ConstructCell(Convert.ToString("Net Weight"), CellValues.String,1));
                            rowselect21.Append(ConstructCell(Convert.ToString("Gross Weight"), CellValues.String,1));
                          //  rowselect21.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect21.Append(ConstructCell(Convert.ToString("Sub Total"), CellValues.String, 2));
                            rowselect21.Append(ConstructCell(Convert.ToString(iExportInvoice.taxable_value), CellValues.String, 1));
                            sheetData.Append(rowselect21);

                            Row rowselect22 = new Row();
                            rowselect22.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect22.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect22.Append(ConstructCell(Convert.ToString(iExportInvoice.no_and_kind_of_pakage), CellValues.String, 2));
                            rowselect22.Append(ConstructCell(Convert.ToString(iExportInvoice.net_weight), CellValues.String, 2));
                            rowselect22.Append(ConstructCell(Convert.ToString(iExportInvoice.gross_weight), CellValues.String, 2));
                           // rowselect22.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect22.Append(ConstructCell(Convert.ToString("Round Off"), CellValues.String, 2));
                            rowselect22.Append(ConstructCell(Convert.ToString(iExportInvoice.round_off), CellValues.String, 1));
                            sheetData.Append(rowselect22);

                            Row rowselect23 = new Row();
                            rowselect23.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect23.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect23.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect22.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect23.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                           rowselect23.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect23.Append(ConstructCell(Convert.ToString("SGST     " + "" + iExportInvoice.sgst_tax_percentage), CellValues.String, 1));
                            rowselect23.Append(ConstructCell(Convert.ToString(iExportInvoice.sgst_value), CellValues.String, 1));
                            sheetData.Append(rowselect23);

                            Row rowselect24 = new Row();
                            rowselect24.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect24.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect24.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect24.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect24.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                           // rowselect24.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                          //  rowselect24.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect24.Append(ConstructCell(Convert.ToString("CGST     " + "" + iExportInvoice.cgst_tax_percentage), CellValues.String, 1));
                            rowselect24.Append(ConstructCell(Convert.ToString(iExportInvoice.cgst_value), CellValues.String, 1));
                            sheetData.Append(rowselect24);

                            Row rowselect25 = new Row();
                            rowselect25.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect25.Append(ConstructCell(Convert.ToString("Total in Words"), CellValues.String,1));
                            rowselect25.Append(ConstructCell(Convert.ToString(ConvertAmountToWords(iExportInvoice.invoice_total)), CellValues.String, 2));
                            rowselect25.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect25.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                           // rowselect25.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            //rowselect25.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect25.Append(ConstructCell(Convert.ToString("Total"), CellValues.String, 2));
                            rowselect25.Append(ConstructCell(Convert.ToString(iExportInvoice.invoice_total), CellValues.String, 2));
                            sheetData.Append(rowselect25);
                            Row rowselec1 = new Row();
                            sheetData.Append(rowselec1);

                            Row rowsele1 = new Row();
                            sheetData.Append(rowsele1);


                            Row rowselect26 = new Row();
                            rowselect26.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect26.Append(ConstructCell(Convert.ToString("Bank Account Details"), CellValues.String,2));
                            rowselect26.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselect26.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect26.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect26.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect26.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            sheetData.Append(rowselect26);

                            Row rowselect27 = new Row();
                            rowselect27.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect27.Append(ConstructCell(Convert.ToString("A/C Name : JJ Traders"), CellValues.String,1));
                            rowselect27.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselect27.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect27.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect27.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect27.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            sheetData.Append(rowselect27);

                            Row rowselect28 = new Row();
                            rowselect28.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect28.Append(ConstructCell(Convert.ToString("Bank : ICICI Bank |A/C No :602305030487"), CellValues.String,1));
                            rowselect28.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselect28.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect28.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect28.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect28.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            sheetData.Append(rowselect28);

                            Row rowselect29 = new Row();
                            rowselect29.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect29.Append(ConstructCell(Convert.ToString("IFCS Code :ICIC0006023"), CellValues.String,1));
                            rowselect29.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselect29.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect29.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect29.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect29.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            sheetData.Append(rowselect29);

                            Row rowselect30 = new Row();
                            rowselect30.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect30.Append(ConstructCell(Convert.ToString("Branch Address :48,Arya gowda road,west mambalam chennai-600033"), CellValues.String,1));
                            rowselect30.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselect30.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect30.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect30.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect30.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            sheetData.Append(rowselect30);

                            Row rowselect31 = new Row();
                            rowselect31.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect31.Append(ConstructCell(Convert.ToString("Branch code : 6032"), CellValues.String,1));
                            rowselect31.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselect31.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect31.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect31.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect31.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            sheetData.Append(rowselect31);

                            Row rowselect32 = new Row();
                            rowselect32.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect32.Append(ConstructCell(Convert.ToString("Swift Code : ICICINBBNRI"), CellValues.String,1));
                            rowselect32.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselect32.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect32.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect32.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect32.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            sheetData.Append(rowselect32);

                            Row rowselect33 = new Row();
                            rowselect33.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect33.Append(ConstructCell(Convert.ToString("AD Code : 6390110"), CellValues.String,1));
                            rowselect33.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselect33.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect33.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                           // rowselect33.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect33.Append(ConstructCell(Convert.ToString("JJ Traders"), CellValues.String, 2));
                            sheetData.Append(rowselect33);

                            Row rowselect34 = new Row();
                            rowselect34.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect34.Append(ConstructCell(Convert.ToString("Declaration"), CellValues.String,2));
                            rowselect34.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselect34.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect34.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect34.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect34.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            sheetData.Append(rowselect34);


                            Row rowselect35 = new Row();
                            rowselect35.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect35.Append(ConstructCell(Convert.ToString("We declare that this invoice shows the actual" +
                                " price of the goods described and that all pariticulars are true and correct."), CellValues.String,1));
                            rowselect35.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            rowselect35.Append(ConstructCell(Convert.ToString(""), CellValues.String,1));
                            rowselect35.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect35.Append(ConstructCell(Convert.ToString(""), CellValues.String, 1));
                            rowselect35.Append(ConstructCell(Convert.ToString(""), CellValues.String, 2));
                            sheetData.Append(rowselect35);

                            worksheetPart.Worksheet.Save();

                        }
                        GeneratedExcelFile = GenerateExportInvoiceExcel(FileLoaction);
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return GeneratedExcelFile;
        }


        public SearchResultBase<ExportInvoiceSearchResultset> SearchExportInvoice(protoSearchParams aprotoSearchParams)
        {
            SearchResultBase<ExportInvoiceSearchResultset> searchResult = new SearchResultBase<ExportInvoiceSearchResultset>();
            try
            {
                DataSet lDataSet = CommonDAL.GetDataSetbyExecuteSP("App_sp_SearchExportInvoice", new string[]
                 { "@SearchParam","@PageNumber","@RowsPerPage"}, new object[]
                { aprotoSearchParams.Keyword,aprotoSearchParams.PageNumber, aprotoSearchParams.RowPerPage});
                if (lDataSet != null)
                {
                    searchResult.SearchResultSet = CommonDAL.SetListFromDataTable<ExportInvoiceSearchResultset>(lDataSet.Tables[0]).OrderByDescending(x => x.export_invoice_id).ToList(); ;
                    searchResult.total_count = Convert.ToInt32(lDataSet.Tables[1].Rows[0][0]);
                    searchResult.page_number = Convert.ToInt32(lDataSet.Tables[1].Rows[0][1]);
                    searchResult.page_size = Convert.ToInt32(lDataSet.Tables[1].Rows[0][2]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
            return searchResult;
        }
    }
}




