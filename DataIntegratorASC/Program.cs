// Decompiled with JetBrains decompiler
// Type: DataIntegratorASC.Program
// Assembly: DataIntegratorASC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CACEFAEB-425A-4474-9813-A6D977B283DD
// Assembly location: C:\Users\morat\OneDrive\Escritorio\DataIntegratorASC.exe

using DataIntegratorASC.Bussiness;
using DataIntegratorASC.Clases;
using System;

namespace DataIntegratorASC
{
    public class Program
    {
        public static void Main(string[] args)
        {
        try
        {
            DateTime dateTime = new DateTime();
            Program.LoadInitialValues();

                MyGlobals.sStepLog = "Catálogos";
                new Catalogos().Import();

                MyGlobals.sStepLog = "WorkOrders";
                Utils.GuardarBitacora("Inicia:" + MyGlobals.sStepLog);
                new OrdenTrabajoBO().Import();

                MyGlobals.sStepLog = "SalesOrders";
                Utils.GuardarBitacora("Inicia:" + MyGlobals.sStepLog);
                new Pedidos().Import();

                MyGlobals.sStepLog = "PurchaseOrders";
                Utils.GuardarBitacora("Inicia:" + MyGlobals.sStepLog);
                new PurchaseOrders().Import();

                Utils.GuardarBitacora("Total de tiempo: " + (DateTime.Now - dateTime).ToString());
            }
        catch (Exception ex)
        {
            if (MyGlobals.oCompany.Connected)
            {
              MyGlobals.oCompany.Disconnect();
            }
            Utils.GuardarBitacora("Error en paso " + MyGlobals.sStepLog + ": " + ex.Message);
        }
    }

    private static void LoadInitialValues()
    {
      try
      {
        MyGlobals.sStepLog = "Carga valores iniciales";
        Utils.GetLoadInitialValues();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
