using Excel;
using LumenWorks.Framework.IO.Csv;
using PracticaF.Models;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PracticaF.Controllers
{
    public class ArchivoController : Controller
    {
        public ActionResult Index(HttpPostedFileBase uploadfile)
        {
            if (ModelState.IsValid)
            {
                if (uploadfile != null && uploadfile.ContentLength > 0)
                {
                    //ExcelDataReader works on binary excel file
                    Stream stream = uploadfile.InputStream;
                    //We need to written the Interface.
                    IExcelDataReader reader = null;
                    if (uploadfile.FileName.EndsWith(".xls"))
                    {
                        //reads the excel file with .xls extension
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (uploadfile.FileName.EndsWith(".xlsx"))
                    {
                        //reads excel file with .xlsx extension
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else if (uploadfile.FileName.EndsWith(".csv"))
                    {

                        DataTable csvTable = new DataTable();
                        using (CsvReader csvReader =
                            new CsvReader(new StreamReader(stream), true))
                        {
                            csvTable.Load(csvReader);
                        }
                        return View(csvTable);
                    }
                    else
                    {
                        //Shows error if uploaded file is not Excel file
                        ModelState.AddModelError("File", "This file format is not supported");
                        return View();
                    }
                    //treats the first row of excel file as Coluymn Names
                    reader.IsFirstRowAsColumnNames = true;
                    //Adding reader data to DataSet()
                    DataSet result = reader.AsDataSet();
                    reader.Close();
                    //Sending result data to View
                    return View(result.Tables[0]);
                }
            }
            else
            {
                ModelState.AddModelError("File", "Please upload your file");
            }

            return View();
        }
    }
}