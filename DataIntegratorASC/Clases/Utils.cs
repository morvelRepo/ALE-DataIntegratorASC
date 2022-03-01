// Decompiled with JetBrains decompiler
// Type: DataIntegratorASC.Clases.Utils
// Assembly: DataIntegratorASC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CACEFAEB-425A-4474-9813-A6D977B283DD
// Assembly location: C:\Users\morat\OneDrive\Escritorio\DataIntegratorASC.exe

using DataIntegratorASC.DomainModel;
using System;
using System.IO;
using System.Reflection;

namespace DataIntegratorASC.Clases
{
  public static class Utils
  {
    public static void GuardarBitacora(string sMensaje)
    {
      try
      {
        string path1 = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\BitacorasApp\\";
        string str = "Bitacora_" + DateTime.Now.ToString("yyyy_MM_dd") + ".txt";
        if (!Directory.Exists(path1))
          Directory.CreateDirectory(path1);
        string path2 = path1 + str;
        if (!File.Exists(path2))
          File.CreateText(path2).Close();
        StreamWriter streamWriter = File.AppendText(path2);
        streamWriter.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " - " + sMensaje);
        streamWriter.Close();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static bool DestroyCOMObject(object oSapObject)
    {
      try
      {
        if (oSapObject != null)
        {
          oSapObject = (object) null;
          GC.Collect();
        }
        return true;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static void GetLoadInitialValues()
    {
      try
      {
        new DBUtils().GetLoadInitialValues();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
