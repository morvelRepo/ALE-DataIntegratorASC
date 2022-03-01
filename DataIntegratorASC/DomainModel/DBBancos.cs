// Decompiled with JetBrains decompiler
// Type: DataIntegratorASC.DomainModel.DBBancos
// Assembly: DataIntegratorASC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CACEFAEB-425A-4474-9813-A6D977B283DD
// Assembly location: C:\Users\morat\OneDrive\Escritorio\DataIntegratorASC.exe

using NucleoBase.Core;
using System;

namespace DataIntegratorASC.DomainModel
{
  public class DBBancos : DBBaseSAP
  {
    public bool ExisteTipoCambioDia()
    {
      try
      {
        object obj = this.oBD_SP.EjecutarValor_DeQuery("SELECT COUNT(1) FROM ORTT WHERE CONVERT(DATE,RateDate) = CONVERT(DATE,(DATEADD(DD,1,GETDATE()))) AND Currency = 'USD'", new object[0]);
        return obj != null && obj.S() == "1";
      }
      catch (Exception ex)
      {
        throw;
      }
    }
  }
}
