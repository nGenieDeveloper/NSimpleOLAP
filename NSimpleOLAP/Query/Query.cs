﻿using NSimpleOLAP.Query.Interfaces;
using System;
using System.Collections.Generic;

namespace NSimpleOLAP.Query
{
  /// <summary>
  /// Description of Query.
  /// </summary>
  public abstract class Query<T> : IQuery<T, IOutputCell<T>>
    where T : struct, IComparable
  {
    protected Axis<T> axis;

    protected IPredicate<T> predicates;

    protected Cube<T> cube;

    protected List<T> measures;

    protected IQueryOrchestrator<T, IOutputCell<T>> queryOrchestrator;

    protected IQueryOrchestrator<T, IOutputCell<T>> Orchestrator
    {
      get
      {
        return queryOrchestrator;
      }
    }

    internal Cube<T> Cube
    {
      get { return cube; }
    }

    internal Axis<T> Axis
    {
      get { return axis; }
    }

    internal List<T> Measures
    {
      get { return measures; }
    }

    internal IPredicate<T> PredicateTree
    {
      get { return predicates; }
    }

    public IEnumerable<IOutputCell<T>> Run()
    {
      return Orchestrator.Run(this);
    }

    public IEnumerable<IOutputCell<T>[]> Run2()
    {
      return Orchestrator.Run2(this);
    }
  }
}