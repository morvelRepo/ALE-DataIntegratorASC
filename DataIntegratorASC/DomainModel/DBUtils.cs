// Decompiled with JetBrains decompiler
// Type: DataIntegratorASC.DomainModel.DBUtils
// Assembly: DataIntegratorASC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CACEFAEB-425A-4474-9813-A6D977B283DD
// Assembly location: C:\Users\morat\OneDrive\Escritorio\DataIntegratorASC.exe

using DataIntegratorASC.Clases;
using NucleoBase.Core;
using SAPbobsCOM;
using System;
using System.Data;
using System.Runtime.InteropServices;

namespace DataIntegratorASC.DomainModel
{
  public class DBUtils
  {
    public void GetLoadInitialValues()
    {
      try
      {
        DataTable dataTable = new DBIntegrator().oBD_SP.EjecutarDT("[Configuracion].[spS_DI_ConsultaAccesosSBO]", new object[0]);
        if (dataTable == null || dataTable.Rows.Count <= 0)
          return;
        DataRow row = dataTable.Rows[0];
                MyGlobals.oCompany = new SAPbobsCOM.Company();

        MyGlobals.oCompany.Server = row.S("Servidor");
        MyGlobals.oCompany.CompanyDB = row.S("DBCompania");
        MyGlobals.oCompany.UserName = row.S("SBOUserName");
        MyGlobals.oCompany.Password = row.S("SBOPassword");
        //MyGlobals.oCompany.LicenseServer = row.S("Licencia");
        MyGlobals.oCompany.SLDServer = "https://admonale:40000/";
        MyGlobals.oCompany.DbUserName = row.S("DBUsuario");
        MyGlobals.oCompany.DbPassword = row.S("DBPassword");
        MyGlobals.oCompany.DbServerType = BoDataServerTypes.dst_MSSQL2019;
        Utils.GuardarBitacora("Intenta conectar a SAP");
        // ISSUE: reference to a compiler-generated method
        int errCode = MyGlobals.oCompany.Connect();
        if (errCode != 0)
        {
          string errMsg = string.Empty;
          // ISSUE: reference to a compiler-generated method
          MyGlobals.oCompany.GetLastError(out errCode, out errMsg);
          MyGlobals.sStepLog = "Conectar: " + errCode.S() + " Mensaje: " + errMsg;
          throw new Exception(MyGlobals.sStepLog);
        }
        Utils.GuardarBitacora("Conectado a SAP con exito!!");
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
