// Decompiled with JetBrains decompiler
// Type: DataIntegratorASC.Objetos.EstatusDocumento
// Assembly: DataIntegratorASC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CACEFAEB-425A-4474-9813-A6D977B283DD
// Assembly location: C:\Users\morat\OneDrive\Escritorio\DataIntegratorASC.exe

namespace DataIntegratorASC.Objetos
{
  public class EstatusDocumento
  {
    private int _iID = 0;
    private string _sEmpresa = string.Empty;
    private string _sTabla = string.Empty;
    private int _iEstatus = 0;
    private string _sMensaje = string.Empty;
    private int _iSapDoc = 0;

    public int iID
    {
      get => this._iID;
      set => this._iID = value;
    }

    public string sEmpresa
    {
      get => this._sEmpresa;
      set => this._sEmpresa = value;
    }

    public string sTabla
    {
      get => this._sTabla;
      set => this._sTabla = value;
    }

    public int iEstatus
    {
      get => this._iEstatus;
      set => this._iEstatus = value;
    }

    public string sMensaje
    {
      get => this._sMensaje;
      set => this._sMensaje = value;
    }

    public int iSapDoc
    {
      get => this._iSapDoc;
      set => this._iSapDoc = value;
    }
  }
}
