
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace SGB_Beta1.Models
{

using System;
    using System.Collections.Generic;
    
public partial class Pedido
{

    public int PedidoID { get; set; }

    public string Produto { get; set; }

    public Nullable<decimal> ValorUnit { get; set; }

    public Nullable<int> Quantia { get; set; }

    public Nullable<decimal> Desconto { get; set; }

    public Nullable<decimal> ValorTotal { get; set; }

    public string Codigo_Produto { get; set; }

}

}
