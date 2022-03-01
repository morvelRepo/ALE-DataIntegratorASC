// Decompiled with JetBrains decompiler
// Type: DataIntegratorASC.DomainModel.DBIntegrator
// Assembly: DataIntegratorASC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CACEFAEB-425A-4474-9813-A6D977B283DD
// Assembly location: C:\Users\morat\OneDrive\Escritorio\DataIntegratorASC.exe

using NucleoBase.BaseDeDatos;
using NucleoBase.Core;

namespace DataIntegratorASC.DomainModel
{
  public class DBIntegrator
  {
    public BD_SP oBD_SP = new BD_SP();

    public DBIntegrator() => this.oBD_SP.sConexionSQL = Globales.GetConfigConnection("SqlIntegrator");
  }
}
