// Decompiled with JetBrains decompiler
// Type: DataIntegratorASC.Objetos.BaseObjeto
// Assembly: DataIntegratorASC, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CACEFAEB-425A-4474-9813-A6D977B283DD
// Assembly location: C:\Users\morat\OneDrive\Escritorio\DataIntegratorASC.exe

using System;
using System.ComponentModel;

namespace DataIntegratorASC.Objetos
{
  [Bindable(BindableSupport.Yes)]
  [Serializable]
  public class BaseObjeto
  {
    private EstatusDocumento _oEstatus = new EstatusDocumento();
    private bool bDisposed = false;

    ~BaseObjeto() => this.Dispose(false);

    [Browsable(false)]
    public EstatusDocumento oEstatus
    {
      get => this._oEstatus;
      set => this._oEstatus = value;
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool bDisposing)
    {
      try
      {
        if (this.bDisposed)
          return;
        if (!bDisposing)
          ;
        this.bDisposed = true;
      }
      catch
      {
      }
    }
  }
}
