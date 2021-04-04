﻿using NSimpleOLAP;
using NSimpleOLAP.Common;
using NSimpleOLAP.Configuration.Fluent;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace UnitTests
{
  [TestFixture]
  public class ReadDataSourceTests
  {
    private Stopwatch _watch;

    public ReadDataSourceTests()
    {
      Init();
    }

    public void Init()
    {
      _watch = new Stopwatch();
    }

    [Test]
    public void DimensionMembersTest()
    {
      CubeBuilder builder = new CubeBuilder();

      builder.SetName("hello")
        .SetSource((sourcebuild) =>
        {
          sourcebuild.SetSource("sales")
            .AddMapping("category", "category")
            .AddMapping("sex", "sex")
            .AddMapping("place", "place");
        })
        .AddDataSource(dsbuild =>
        {
          dsbuild.SetName("sales")
            .SetSourceType(DataSourceType.CSV)
            .SetCSVConfig(csvbuild =>
            {
              csvbuild.SetFilePath("TestData//table.csv")
                               .SetHasHeader();
            })
            .AddField("category", 0, typeof(int))
            .AddField("sex", 1, typeof(int))
            .AddField("place", 2, typeof(int))
            .AddField("expenses", 3, typeof(double))
            .AddField("items", 4, typeof(int));
        })
        .AddDataSource(dsbuild =>
        {
          dsbuild.SetName("categories")
            .SetSourceType(DataSourceType.CSV)
            .AddField("id", 0, typeof(int))
            .AddField("description", 1, typeof(string))
            .SetCSVConfig(csvbuild =>
            {
              csvbuild.SetFilePath("TestData//dimension1.csv")
                               .SetHasHeader();
            });
        })
        .AddDataSource(dsbuild =>
        {
          dsbuild.SetName("sexes")
            .SetSourceType(DataSourceType.CSV)
            .AddField("id", 0, typeof(int))
            .AddField("description", 1, typeof(string))
            .SetCSVConfig(csvbuild =>
            {
              csvbuild.SetFilePath("TestData//dimension2.csv")
                               .SetHasHeader();
            });
        })
        .AddDataSource(dsbuild =>
        {
          dsbuild.SetName("places")
            .SetSourceType(DataSourceType.CSV)
            .AddField("id", 0, typeof(int))
            .AddField("description", 1, typeof(string))
            .SetCSVConfig(csvbuild =>
            {
              csvbuild.SetFilePath("TestData//dimension3.csv")
                               .SetHasHeader();
            });
        })
        .MetaData(mbuild =>
        {
          mbuild.AddDimension("category", (dimbuild) =>
          {
            dimbuild.Source("categories")
              .ValueField("id")
              .DescField("description");
          })
            .AddDimension("sex", (dimbuild) =>
            {
              dimbuild.Source("sexes")
                          .ValueField("id")
                          .DescField("description");
            })
            .AddDimension("place", (dimbuild) =>
            {
              dimbuild.Source("places")
                          .ValueField("id")
                          .DescField("description");
            })
            .AddMeasure("spent", mesbuild =>
            {
              mesbuild.ValueField("expenses")
                          .SetType(typeof(double));
            })
            .AddMeasure("quantity", mesbuild =>
            {
              mesbuild.ValueField("items")
                          .SetType(typeof(int));
            });
        });

      KeyValuePair<int, int>[] pairs = new KeyValuePair<int, int>[] {
        new KeyValuePair<int, int>(1,2),
        new KeyValuePair<int, int>(2,1),
        new KeyValuePair<int, int>(3,6) };
      KeyValuePair<int, int>[] pairs2 = new KeyValuePair<int, int>[] {
        new KeyValuePair<int, int>(2,1),
        new KeyValuePair<int, int>(3,6) };
      Cube<int> cube = builder.Create<int>();

      _watch.Reset();
      _watch.Start();

      cube.Initialize();
      cube.Process();

      Cell<int> xcell = cube.Cells[pairs];
      Cell<int> xcell2 = cube.Cells[pairs2];

      _watch.Stop();
      Console.WriteLine();
      Console.WriteLine(_watch.ElapsedMilliseconds);
      Console.WriteLine();

      Assert.AreEqual("male", cube.Schema.Dimensions["sex"].Members["male"].Name);
      Assert.AreEqual("female", cube.Schema.Dimensions["sex"].Members["female"].Name);
      Assert.AreEqual("London", cube.Schema.Dimensions["place"].Members["London"].Name);
      Assert.AreEqual(5, xcell.Values[cube.Schema.Measures["quantity"].ID]);
      Assert.AreEqual(10.10, xcell.Values[cube.Schema.Measures["spent"].ID]);
      Assert.AreEqual(3, xcell2.Occurrences);
    }

    [Test]
    public void CellsEnumeratorTest()
    {
      CubeBuilder builder = new CubeBuilder();

      builder.SetName("hello")
        .SetSource((sourcebuild) =>
        {
          sourcebuild.SetSource("sales")
            .AddMapping("category", "category")
            .AddMapping("sex", "sex")
            .AddMapping("place", "place");
        })
        .AddDataSource(dsbuild =>
        {
          dsbuild.SetName("sales")
            .SetSourceType(DataSourceType.CSV)
            .SetCSVConfig(csvbuild =>
            {
              csvbuild.SetFilePath("TestData//table.csv")
                               .SetHasHeader();
            })
            .AddField("category", 0, typeof(int))
            .AddField("sex", 1, typeof(int))
            .AddField("place", 2, typeof(int))
            .AddField("expenses", 3, typeof(double))
            .AddField("items", 4, typeof(int));
        })
        .AddDataSource(dsbuild =>
        {
          dsbuild.SetName("categories")
            .SetSourceType(DataSourceType.CSV)
            .AddField("id", 0, typeof(int))
            .AddField("description", 1, typeof(string))
            .SetCSVConfig(csvbuild =>
            {
              csvbuild.SetFilePath("TestData//dimension1.csv")
                               .SetHasHeader();
            });
        })
        .AddDataSource(dsbuild =>
        {
          dsbuild.SetName("sexes")
            .SetSourceType(DataSourceType.CSV)
            .AddField("id", 0, typeof(int))
            .AddField("description", 1, typeof(string))
            .SetCSVConfig(csvbuild =>
            {
              csvbuild.SetFilePath("TestData//dimension2.csv")
                               .SetHasHeader();
            });
        })
        .AddDataSource(dsbuild =>
        {
          dsbuild.SetName("places")
            .SetSourceType(DataSourceType.CSV)
            .AddField("id", 0, typeof(int))
            .AddField("description", 1, typeof(string))
            .SetCSVConfig(csvbuild =>
            {
              csvbuild.SetFilePath("TestData//dimension3.csv")
                               .SetHasHeader();
            });
        })
        .MetaData(mbuild =>
        {
          mbuild.AddDimension("category", (dimbuild) =>
          {
            dimbuild.Source("categories")
              .ValueField("id")
              .DescField("description");
          })
            .AddDimension("sex", (dimbuild) =>
            {
              dimbuild.Source("sexes")
                          .ValueField("id")
                          .DescField("description");
            })
            .AddDimension("place", (dimbuild) =>
            {
              dimbuild.Source("places")
                          .ValueField("id")
                          .DescField("description");
            })
            .AddMeasure("spent", mesbuild =>
            {
              mesbuild.ValueField("expenses")
                          .SetType(typeof(double));
            })
            .AddMeasure("quantity", mesbuild =>
            {
              mesbuild.ValueField("items")
                          .SetType(typeof(int));
            });
        });

      Cube<int> cube = builder.Create<int>();

      _watch.Reset();
      _watch.Start();

      cube.Initialize();
      cube.Process();
      // 12, 7
      Console.WriteLine();

      foreach (Cell<int> item in cube.Cells)
      {
        foreach (var pair in item.Coords)
        {
          Console.Write(pair.Key);
          Console.Write(",");
          Console.Write(pair.Value);
          Console.Write("|");
        }
        Console.WriteLine();
      }

      int count = cube.Cells.Count;

      _watch.Stop();
      Console.WriteLine();
      Console.WriteLine(_watch.ElapsedMilliseconds);
      Console.WriteLine();
    }
  }
}