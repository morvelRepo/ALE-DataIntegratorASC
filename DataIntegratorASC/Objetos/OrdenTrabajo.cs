// Decompiled with JetBrains decompiler
// Type: DataIntegratorASC.Objetos.OrdenTrabajo
// Assembly: DataIntegratorASC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CACEFAEB-425A-4474-9813-A6D977B283DD
// Assembly location: C:\Users\morat\OneDrive\Escritorio\DataIntegratorASC.exe

using System;
using System.Collections.Generic;

namespace DataIntegratorASC.Objetos
{
  public class OrdenTrabajo : BaseObjeto
  {
    private string _sCardCode = string.Empty;
    private string _sSerie = string.Empty;
    private string _sPedido = string.Empty;
    private string _sEstado = string.Empty;
    private string _sFechaPedido = string.Empty;
    private string _sFechaPrometida = string.Empty;
    private string _sFechaProxEmbarque = string.Empty;
    private string _sOrdenCompra = string.Empty;
    private string _sFechaOrden = string.Empty;
    private string _sEmbarcar_a = string.Empty;
    private string _sClaveDirEmbarque = string.Empty;
    private string _sDireccionFactura = string.Empty;
    private string _sRubro1 = string.Empty;
    private string _sRubro2 = string.Empty;
    private string _sRubro3 = string.Empty;
    private string _sRubro4 = string.Empty;
    private string _sComentario_cxc = string.Empty;
    private Decimal _dTotalMercaderia = 0M;
    private Decimal _dTotalImpuesto1 = 0M;
    private Decimal _dTotalImpuesto2 = 0M;
    private Decimal _dTotalAFacturar = 0M;
    private string _sMoneda = string.Empty;
    private int _iCliente = 0;
    private int _iClienteDireccion = 0;
    private int _iClienteCorporacion = 0;
    private int _iClienteOrigen = 0;
    private string _sPais = string.Empty;
    private string _sDescDirEmbarque = string.Empty;
    private Decimal _dBaseImpuesto1 = 0M;
    private Decimal _dBaseImpuesto2 = 0M;
    private string _sNombreCliente = string.Empty;
    private string _sFechaProyectada = string.Empty;
    private string _sRFC = string.Empty;
    private bool _IsIvaExempt = false;
    private string _sBase = string.Empty;
    private List<LineasOrdenTrabajo> _olsLineas = new List<LineasOrdenTrabajo>();
    private string _Site = string.Empty;
    private string _sProyectoH = string.Empty;

    public string sCardCode
    {
      set => this._sCardCode = value;
      get => this._sCardCode;
    }

    public string sSerie
    {
      set => this._sSerie = value;
      get => this._sSerie;
    }

    public string sPedido
    {
      set => this._sPedido = value;
      get => this._sPedido;
    }

    public string sEstado
    {
      set => this._sEstado = value;
      get => this._sEstado;
    }

    public string sFechaPedido
    {
      set => this._sFechaPedido = value;
      get => this._sFechaPedido;
    }

    public string sFechaPrometida
    {
      set => this._sFechaPrometida = value;
      get => this._sFechaPrometida;
    }

    public string sFechaProxEmbarque
    {
      set => this._sFechaProxEmbarque = value;
      get => this._sFechaProxEmbarque;
    }

    public string sOrdenCompra
    {
      set => this._sOrdenCompra = value;
      get => this._sOrdenCompra;
    }

    public string sFechaOrden
    {
      set => this._sFechaOrden = value;
      get => this._sFechaOrden;
    }

    public string sEmbarcar_a
    {
      set => this._sEmbarcar_a = value;
      get => this._sEmbarcar_a;
    }

    public string sClaveDirEmbarque
    {
      set => this._sClaveDirEmbarque = value;
      get => this._sClaveDirEmbarque;
    }

    public string sDireccionFactura
    {
      set => this._sDireccionFactura = value;
      get => this._sDireccionFactura;
    }

    public string sRubro1
    {
      set => this._sRubro1 = value;
      get => this._sRubro1;
    }

    public string sRubro2
    {
      set => this._sRubro2 = value;
      get => this._sRubro2;
    }

    public string sRubro3
    {
      set => this._sRubro3 = value;
      get => this._sRubro3;
    }

    public string sRubro4
    {
      set => this._sRubro4 = value;
      get => this._sRubro4;
    }

    public string sComentario_cxc
    {
      set => this._sComentario_cxc = value;
      get => this._sComentario_cxc;
    }

    public Decimal dTotalMercaderia
    {
      set => this._dTotalMercaderia = value;
      get => this._dTotalMercaderia;
    }

    public Decimal dTotalImpuesto1
    {
      set => this._dTotalImpuesto1 = value;
      get => this._dTotalImpuesto1;
    }

    public Decimal dTotalImpuesto2
    {
      set => this._dTotalImpuesto2 = value;
      get => this._dTotalImpuesto2;
    }

    public Decimal dTotalAFacturar
    {
      set => this._dTotalAFacturar = value;
      get => this._dTotalAFacturar;
    }

    public string sMoneda
    {
      set => this._sMoneda = value;
      get => this._sMoneda;
    }

    public int iCliente
    {
      set => this._iCliente = value;
      get => this._iCliente;
    }

    public int iClienteDireccion
    {
      set => this._iClienteDireccion = value;
      get => this._iClienteDireccion;
    }

    public int iClienteCorporacion
    {
      set => this._iClienteCorporacion = value;
      get => this._iClienteCorporacion;
    }

    public int iClienteOrigen
    {
      set => this._iClienteOrigen = value;
      get => this._iClienteOrigen;
    }

    public string sPais
    {
      set => this._sPais = value;
      get => this._sPais;
    }

    public string sDescDirEmbarque
    {
      set => this._sDescDirEmbarque = value;
      get => this._sDescDirEmbarque;
    }

    public Decimal dBaseImpuesto1
    {
      set => this._dBaseImpuesto1 = value;
      get => this._dBaseImpuesto1;
    }

    public Decimal dBaseImpuesto2
    {
      set => this._dBaseImpuesto2 = value;
      get => this._dBaseImpuesto2;
    }

    public string sNombreCliente
    {
      set => this._sNombreCliente = value;
      get => this._sNombreCliente;
    }

    public string sFechaProyectada
    {
      set => this._sFechaProyectada = value;
      get => this._sFechaProyectada;
    }

    public string sRFC
    {
      set => this._sRFC = value;
      get => this._sRFC;
    }

    public bool IsIvaExempt
    {
      set => this._IsIvaExempt = value;
      get => this._IsIvaExempt;
    }

    public string sBase
    {
      set => this._sBase = value;
      get => this._sBase;
    }

    public List<LineasOrdenTrabajo> olsLineas
    {
      set => this._olsLineas = value;
      get => this._olsLineas;
    }

    public string Site
    {
      set => this._Site = value;
      get => this._Site;
    }

    public string sProyectoH
    {
      set => this._sProyectoH = value;
      get => this._sProyectoH;
    }
  }
}
