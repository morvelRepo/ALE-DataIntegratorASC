// Decompiled with JetBrains decompiler
// Type: DataIntegratorASC.DomainModel.DBAccesoSAP
// Assembly: DataIntegratorASC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CACEFAEB-425A-4474-9813-A6D977B283DD
// Assembly location: C:\Users\morat\OneDrive\Escritorio\DataIntegratorASC.exe

using DataIntegratorASC.Clases;
using Microsoft.CSharp.RuntimeBinder;
using NucleoBase.Core;
using SAPbobsCOM;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DataIntegratorASC.DomainModel
{
  public class DBAccesoSAP : DBBaseSAP
  {
    public DataSet DBGetObtieneDatosDeQuery(string sQuery)
    {
      try
      {
        SqlConnection connection = new SqlConnection();
        try
        {
          DataSet dataSet = new DataSet();
          connection.ConnectionString = this.oBD_SP.sConexionSQL;
          connection.Open();
          SqlCommand selectCommand = new SqlCommand(sQuery, connection);
          selectCommand.CommandType = CommandType.Text;
          selectCommand.CommandTimeout = 0;
          new SqlDataAdapter(selectCommand).Fill(dataSet);
          connection.Close();
          return dataSet;
        }
        catch (Exception ex)
        {
          throw ex;
        }
        finally
        {
          connection?.Dispose();
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public string DBGetObtieneCodigoEstado(string sEstado)
    {
      try
      {
        string str = string.Empty;
        if (sEstado == "Edo. DE Mexico" || sEstado == "ESTADO DE MEXICO")
          str = "MEX";
        return str == string.Empty ? this.oBD_SP.EjecutarValor_DeQuery("SELECT Code FROM OCST (NOLOCK) WHERE [Name] = '" + sEstado + "'", new object[0]).S() : str;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public string DBGetObtieneCodigoPais(string sEstado)
    {
      try
      {
        string str = string.Empty;
        if (sEstado == "Estados Unidos de America")
          str = "US";
        return str == string.Empty ? this.oBD_SP.EjecutarValor_DeQuery("SELECT Code FROM OCRY (NOLOCK) WHERE [Name] = '" + sEstado + "'", new object[0]).S() : str;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public object GetValueByQuery(string sQ)
    {
      
      Recordset o = MyGlobals.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
      object valueByQuery = (object) null;
      try
      {
        // ISSUE: reference to a compiler-generated method
        o.DoQuery(sQ);
        if (o.RecordCount > 0)
        {
          // ISSUE: reference to a compiler-generated method
          valueByQuery = o.Fields.Item((object) 0).Value;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        if (o != null)
        {
          Marshal.ReleaseComObject((object) o);
          GC.Collect();
        }
      }
      return valueByQuery;
    }
  }
}
