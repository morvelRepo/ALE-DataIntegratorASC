// Decompiled with JetBrains decompiler
// Type: DataIntegratorASC.Objetos.DireccionSocioNegocio
// Assembly: DataIntegratorASC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CACEFAEB-425A-4474-9813-A6D977B283DD
// Assembly location: C:\Users\morat\OneDrive\Escritorio\DataIntegratorASC.exe

using SAPbobsCOM;

namespace DataIntegratorASC.Objetos
{
  public class DireccionSocioNegocio
  {
    private BoAddressType _oAdressType = BoAddressType.bo_BillTo;
    private string _sStreetNumber = string.Empty;
    private string _sBlockColonia = string.Empty;
    private string _sCiudad = string.Empty;
    private string _sCodigoPostal = string.Empty;
    private string _sState = string.Empty;
    private string _sCountry = string.Empty;
    private string _sCounty = string.Empty;
    private string _sStreetNo = string.Empty;
    private string _sBuilding = string.Empty;

    public BoAddressType oAdressType
    {
      set => this._oAdressType = value;
      get
      {
        BoAddressType oAdressType = this._oAdressType;
        return oAdressType;
      }
    }

    public string sStreetNumber
    {
      set => this._sStreetNumber = value;
      get => this._sStreetNumber;
    }

    public string sBlockColonia
    {
      set => this._sBlockColonia = value;
      get => this._sBlockColonia;
    }

    public string sCiudad
    {
      set => this._sCiudad = value;
      get => this._sCiudad;
    }

    public string sCodigoPostal
    {
      set => this._sCodigoPostal = value;
      get => this._sCodigoPostal;
    }

    public string sState
    {
      set => this._sState = value;
      get => this._sState;
    }

    public string sCountry
    {
      set => this._sCountry = value;
      get => this._sCountry;
    }

    public string sCounty
    {
      set => this._sCounty = value;
      get => this._sCounty;
    }

    public string sStreetNo
    {
      set => this._sStreetNo = value;
      get => this._sStreetNo;
    }

    public string sBuilding
    {
      set => this._sBuilding = value;
      get => this._sBuilding;
    }
  }
}
