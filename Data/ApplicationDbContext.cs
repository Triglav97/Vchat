using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vchat.Models;

namespace Vchat.Data{
    public class ApplicationDbContext : IdentityDbContext <User>{
        
        public DbSet<CryptKeysModel> CryptKeys { get; set; }
        public DbSet<CryptMessagesModel> CryptMessages { get; set; }
        public DbSet<MyContactsModel> MyContacts { get; set; }
        public ApplicationDbContext(){
                Database.EnsureCreated();
        }        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            optionsBuilder.UseSqlite(@"DataSource=app.db;Cache=Shared");
        }
    }
}
