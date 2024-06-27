﻿using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Specification
{
    public class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery , ISpecification<T> specification)
        {
            var query = inputQuery.AsQueryable();
            if(specification.Predicate != null)
            {
                query=query.Where(specification.Predicate);
            }
            if(specification.Includes.Any())
            {
                query=specification.Includes.Aggregate(query , (current,value)=>current.Include(value));
            }
            return query;
        }
    }
} 