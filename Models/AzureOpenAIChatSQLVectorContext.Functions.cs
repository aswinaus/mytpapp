﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using AzureOpenAIChat.Models;

namespace AzureOpenAIChat.Models
{
    public partial class AzureOpenAIChatSQLVectorContext
    {

        [DbFunction("SimilarContentArticles", "dbo")]
        public IQueryable<SimilarContentArticlesResult> SimilarContentArticles(string vector)
        {
            return FromExpression(() => SimilarContentArticles(vector));
        }

        protected void OnModelCreatingGeneratedFunctions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SimilarContentArticlesResult>().HasNoKey();
        }
    }
}