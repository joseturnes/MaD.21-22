

//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------


namespace Es.Udc.DotNet.PracticaMaD.Model
{

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class photogramEntities1 : DbContext
{
    public photogramEntities1()
        : base("name=photogramEntities1")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<Category> Category { get; set; }

    public virtual DbSet<Comment> Comment { get; set; }

    public virtual DbSet<ImageUpload> ImageUpload { get; set; }

    public virtual DbSet<Tag> Tag { get; set; }

    public virtual DbSet<UserProfile> UserProfile { get; set; }

}

}

