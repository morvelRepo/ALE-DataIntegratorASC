// Decompiled with JetBrains decompiler
// Type: DataIntegratorASC.Bussiness.Bancos
// Assembly: DataIntegratorASC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CACEFAEB-425A-4474-9813-A6D977B283DD
// Assembly location: C:\Users\morat\OneDrive\Escritorio\DataIntegratorASC.exe

using DataIntegratorASC.Clases;
using DataIntegratorASC.DomainModel;
using Microsoft.CSharp.RuntimeBinder;
using NucleoBase.Core;
using SAPbobsCOM;
using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace DataIntegratorASC.Bussiness
{
  public class Bancos
  {
    public void ObtieneTipoCambioDia()
    {
      try
      {
        DateTime now = DateTime.Now;
        if (now.Hour <= 4 || now.DayOfWeek == DayOfWeek.Saturday || now.DayOfWeek == DayOfWeek.Sunday)
          return;
        Utils.GuardarBitacora("Comienza: Actualizar Tipo de Cambio");
        if (!new DBBancos().ExisteTipoCambioDia())
        {
          XmlTextReader xmlTextReader = new XmlTextReader("http://www.dof.gob.mx/indicadores.xml");
          bool flag = false;
          string empty = string.Empty;
          string str1 = string.Empty;
          while (xmlTextReader.Read())
          {
            switch (xmlTextReader.NodeType)
            {
              case XmlNodeType.Element:
                Console.Write("<" + xmlTextReader.Name);
                while (xmlTextReader.MoveToNextAttribute())
                  Console.Write(" " + xmlTextReader.Name + "='" + xmlTextReader.Value + "'");
                Console.Write(">");
                Console.WriteLine(">");
                break;
              case XmlNodeType.Text:
                Console.WriteLine(xmlTextReader.Value);
                if (flag && empty == string.Empty)
                {
                  empty = xmlTextReader.Value;
                  break;
                }
                if (flag && empty != string.Empty && str1 == string.Empty)
                {
                  str1 = xmlTextReader.Value.S();
                  break;
                }
                break;
              case XmlNodeType.EndElement:
                Console.Write("</" + xmlTextReader.Name);
                Console.WriteLine(">");
                break;
            }
            if (xmlTextReader.Value == "DOLAR")
              flag = true;
          }
          string str2 = now.Day.S().PadLeft(2, '0') + "/" + now.Month.S().PadLeft(2, '0') + "/" + now.Year.S().Substring(2, 2);
                    if (str1 == str2)
                    {
            

                        SAPbobsCOM.SBObob sbObob = MyGlobals.oCompany.GetBusinessObject(BoObjectTypes.BoBridge);

                        double num = empty.Replace('.', ',').S().Db();
                        if (num > 100.0)
                            num = Convert.ToDouble(empty);
                        if (!string.IsNullOrEmpty(empty))
                        {
                            // ISSUE: reference to a compiler-generated method
                            sbObob.SetCurrencyRate("USD", DateTime.Now.AddDays(1.0), num, true);
                            if (now.DayOfWeek == DayOfWeek.Friday)
                            {
                            // ISSUE: reference to a compiler-generated method
                            sbObob.SetCurrencyRate("USD", DateTime.Now.AddDays(2.0), num, true);
                            // ISSUE: reference to a compiler-generated method
                            sbObob.SetCurrencyRate("USD", DateTime.Now.AddDays(3.0), num, true);
                            }
                            Utils.GuardarBitacora("Tipo de Cambio Actualizado en SAP");
                        }
                    }
        }
      }
      catch (Exception ex)
      {
        throw;
      }
    }
  }
}
