// Decompiled with JetBrains decompiler
// Type: DataIntegratorASC.Objetos.SocioNegocio
// Assembly: DataIntegratorASC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CACEFAEB-425A-4474-9813-A6D977B283DD
// Assembly location: C:\Users\morat\OneDrive\Escritorio\DataIntegratorASC.exe

using SAPbobsCOM;
using System.Collections.Generic;

namespace DataIntegratorASC.Objetos
{
  public class SocioNegocio : BaseObjeto
  {
    private int _iCompanyKeyOri = 0;
    private string _sCardCode = string.Empty;
    private string _sCardName = string.Empty;
    private BoCardTypes _oTipoBP = BoCardTypes.cCustomer;
    private string _CardFName = string.Empty;
    private int _iGroupCode = 0;
    private string _sCurrency = string.Empty;
    private string _sLicTradNum = string.Empty;
    private string _sPhone1 = string.Empty;
    private string _sPhone2 = string.Empty;
    private string _sCellular = string.Empty;
    private string _sFax = string.Empty;
    private string _sEmail = string.Empty;
    private string _sIntrntSite = string.Empty;
    private string _cmpPrivate = string.Empty;
    private string _sVatInUnCmp = string.Empty;
    private string _sNotes = string.Empty;
    private string _sAliasName = string.Empty;
    private long _lLangCode = 0;
    private string _sActivo = string.Empty;
    private double _dbCreditLine = 0.0;
    private double _dbDebtLine = 0.0;
    private string _sBase = string.Empty;
    private string _sQryGroup1 = string.Empty;
    private string _sQryGroup2 = string.Empty;
    private List<DireccionSocioNegocio> _oLstDir = new List<DireccionSocioNegocio>();

    public int iCompanyKeyOri
    {
      set => this._iCompanyKeyOri = value;
      get => this._iCompanyKeyOri;
    }

    public string sCardCode
    {
      set => this._sCardCode = value;
      get => this._sCardCode;
    }

    public string sCardName
    {
      set => this._sCardName = value;
      get => this._sCardName;
    }

    public BoCardTypes oTipoBP
    {
      set => this._oTipoBP = value;
      get
      {
        BoCardTypes oTipoBp = this._oTipoBP;
        return oTipoBp;
      }
    }

    public string CardFName
    {
      set => this._CardFName = value;
      get => this._CardFName;
    }

    public int iGroupCode
    {
      set => this._iGroupCode = value;
      get => this._iGroupCode;
    }

    public string sCurrency
    {
      set => this._sCurrency = value;
      get => this._sCurrency;
    }

    public string sLicTradNumRFC
    {
      set => this._sLicTradNum = value;
      get => this._sLicTradNum;
    }

    public string sPhone1
    {
      set => this._sPhone1 = value;
      get => this._sPhone1;
    }

    public string sPhone2
    {
      set => this._sPhone2 = value;
      get => this._sPhone2;
    }

    public string sCellular
    {
      set => this._sCellular = value;
      get => this._sCellular;
    }

    public string sFax
    {
      set => this._sFax = value;
      get => this._sFax;
    }

    public string sEmail
    {
      set => this._sEmail = value;
      get => this._sEmail;
    }

    public string sIntrntSite
    {
      set => this._sIntrntSite = value;
      get => this._sIntrntSite;
    }

    public string cmpPrivate
    {
      set => this._cmpPrivate = value;
      get => this._cmpPrivate;
    }

    public string sVatInUnCmp
    {
      set => this._sVatInUnCmp = value;
      get => this._sVatInUnCmp;
    }

    public string sNotes
    {
      set => this._sNotes = value;
      get => this._sNotes;
    }

    public string sAliasName
    {
      set => this._sAliasName = value;
      get => this._sAliasName;
    }

    public long lLangCode
    {
      set => this._lLangCode = value;
      get => this._lLangCode;
    }

    public string sActivo
    {
      set => this._sActivo = value;
      get => this._sActivo;
    }

    public double dbCreditLine
    {
      set => this._dbCreditLine = value;
      get => this._dbCreditLine;
    }

    public double dbDebtLine
    {
      set => this._dbDebtLine = value;
      get => this._dbDebtLine;
    }

    public string sBase
    {
      set => this._sBase = value;
      get => this._sBase;
    }

    public string sQryGroup1
    {
      set => this._sQryGroup1 = value;
      get => this._sQryGroup1;
    }

    public string sQryGroup2
    {
      set => this._sQryGroup2 = value;
      get => this._sQryGroup2;
    }

    public List<DireccionSocioNegocio> oLstDir
    {
      set => this._oLstDir = value;
      get => this._oLstDir;
    }
  }
}
