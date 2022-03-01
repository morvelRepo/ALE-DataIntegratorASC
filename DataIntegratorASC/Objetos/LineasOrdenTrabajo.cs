// Decompiled with JetBrains decompiler
// Type: DataIntegratorASC.Objetos.LineasOrdenTrabajo
// Assembly: DataIntegratorASC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CACEFAEB-425A-4474-9813-A6D977B283DD
// Assembly location: C:\Users\morat\OneDrive\Escritorio\DataIntegratorASC.exe

using System;

namespace DataIntegratorASC.Objetos
{
  public class LineasOrdenTrabajo
  {
    private string _sPedido = string.Empty;
    private int _iPedidoLinea = 0;
    private string _sBodega = string.Empty;
    private string _sArticulo = string.Empty;
    private string _sEstado = string.Empty;
    private string _sFechaEntrega = string.Empty;
    private int _iLineaUsuario = 0;
    private Decimal _dPrecioUnitario = 0M;
    private int _iCantidadPedida = 0;
    private int _iCantidadFacturar = 0;
    private Decimal _dPorcDescuento = 0M;
    private string _sDescripcion = string.Empty;
    private string _sComentario = string.Empty;
    private string _sFechaPrometida = string.Empty;
    private int _iLineaOrdenCompra = 0;
    private string _sCentroCosto = string.Empty;
    private string _sCuentaContable = string.Empty;
    private string _sDimension1 = string.Empty;
    private string _sDimension2 = string.Empty;
    private string _sDimension3 = string.Empty;
    private string _sDimension4 = string.Empty;
    private string _sDimension5 = string.Empty;
    private string _sProyecto = string.Empty;
    private string _sUbicacion = string.Empty;

    public string sPedido
    {
      set => this._sPedido = value;
      get => this._sPedido;
    }

    public int iPedidoLinea
    {
      set => this._iPedidoLinea = value;
      get => this._iPedidoLinea;
    }

    public string sBodega
    {
      set => this._sBodega = value;
      get => this._sBodega;
    }

    public string sArticulo
    {
      set => this._sArticulo = value;
      get => this._sArticulo;
    }

    public string sEstado
    {
      set => this._sEstado = value;
      get => this._sEstado;
    }

    public string sFechaEntrega
    {
      set => this._sFechaEntrega = value;
      get => this._sFechaEntrega;
    }

    public int iLineaUsuario
    {
      set => this._iLineaUsuario = value;
      get => this._iLineaUsuario;
    }

    public Decimal dPrecioUnitario
    {
      set => this._dPrecioUnitario = value;
      get => this._dPrecioUnitario;
    }

    public int iCantidadPedida
    {
      set => this._iCantidadPedida = value;
      get => this._iCantidadPedida;
    }

    public int iCantidadFacturar
    {
      set => this._iCantidadFacturar = value;
      get => this._iCantidadFacturar;
    }

    public Decimal dPorcDescuento
    {
      set => this._dPorcDescuento = value;
      get => this._dPorcDescuento;
    }

    public string sDescripcion
    {
      set => this._sDescripcion = value;
      get => this._sDescripcion;
    }

    public string sComentario
    {
      set => this._sComentario = value;
      get => this._sComentario;
    }

    public string sFechaPrometida
    {
      set => this._sFechaPrometida = value;
      get => this._sFechaPrometida;
    }

    public int iLineaOrdenCompra
    {
      set => this._iLineaOrdenCompra = value;
      get => this._iLineaOrdenCompra;
    }

    public string sCentroCosto
    {
      set => this._sCentroCosto = value;
      get => this._sCentroCosto;
    }

    public string sCuentaContable
    {
      set => this._sCuentaContable = value;
      get => this._sCuentaContable;
    }

    public string sDimension1
    {
      set => this._sDimension1 = value;
      get => this._sDimension1;
    }

    public string sDimension2
    {
      set => this._sDimension2 = value;
      get => this._sDimension2;
    }

    public string sDimension3
    {
      set => this._sDimension3 = value;
      get => this._sDimension3;
    }

    public string sDimension4
    {
      set => this._sDimension4 = value;
      get => this._sDimension4;
    }

    public string sDimension5
    {
      set => this._sDimension5 = value;
      get => this._sDimension5;
    }

    public string sProyecto
    {
      set => this._sProyecto = value;
      get => this._sProyecto;
    }

    public string sUbicacion
    {
      set => this._sUbicacion = value;
      get => this._sUbicacion;
    }
  }
}
