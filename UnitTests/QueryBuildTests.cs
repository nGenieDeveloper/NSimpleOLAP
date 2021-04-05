using NSimpleOLAP;
using NSimpleOLAP.Common;
using NSimpleOLAP.Configuration.Fluent;
using NSimpleOLAP.Query;
using NSimpleOLAP.Query.Builder;
using NUnit.Framework;

namespace UnitTests
{
  [TestFixture]
  public class QueryBuildTests
  {
    private Cube<int> cube;

    public QueryBuildTests()
    {
      Init();
    }

    public void Init()
    {
      CubeBuilder builder = new CubeBuilder();

      builder.SetName("hello")
        .SetSource((sourcebuild) => sourcebuild.SetSource("sales"))
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

      cube = builder.Create<int>();
      cube.Initialize();
      cube.Process();
    }

    [OneTimeTearDown]
    public void Dispose()
    {
      cube.Dispose();
    }

    [Test]
    public void WhereBuilder_Setup_Test()
    {
      Assert.DoesNotThrow(() =>
      {
        WhereBuilder<int> builder = new WhereBuilder<int>(cube.Schema
        , new DimensionReferenceTranslator<int>(cube.Schema),
        new MeasureReferenceTranslator<int>(cube.Schema));
      });
    }

    [Test]
    public void WhereBuilder_Add_MeasureSlicer_Test()
    {
      WhereBuilder<int> builder = new WhereBuilder<int>(cube.Schema
        , new DimensionReferenceTranslator<int>(cube.Schema),
        new MeasureReferenceTranslator<int>(cube.Schema));

      builder.Define(b => b.Measure("quantity").GreaterOrEquals(2));

      var predicate = builder.Build();

      Assert.IsNotNull(predicate);
      Assert.IsFalse(predicate.FiltersOnAggregation());
    }

    [Test]
    public void WhereBuilder_Add_DimensionSlicer_Test()
    {
      WhereBuilder<int> builder = new WhereBuilder<int>(cube.Schema
        , new DimensionReferenceTranslator<int>(cube.Schema),
        new MeasureReferenceTranslator<int>(cube.Schema));

      builder.Define(b => b.Dimension("category").IsEquals("clothes"));

      var predicate = builder.Build();

      Assert.IsNotNull(predicate);
      Assert.IsTrue(predicate.FiltersOnAggregation());
    }

    [Test]
    public void WhereBuilder_Add_And_Expression_With_DimensionSlicers_Test()
    {
      WhereBuilder<int> builder = new WhereBuilder<int>(cube.Schema
        , new DimensionReferenceTranslator<int>(cube.Schema),
        new MeasureReferenceTranslator<int>(cube.Schema));

      builder.Define(b =>
        b.And(x => x.Dimension("category").IsEquals("clothes"),
        x => x.Dimension("category").IsEquals("shoes")));

      var predicate = builder.Build();

      Assert.IsNotNull(predicate);
      Assert.IsTrue(predicate.FiltersOnAggregation());
    }

    [Test]
    public void WhereBuilder_Add_Or_Expression_With_DimensionSlicers_Test()
    {
      WhereBuilder<int> builder = new WhereBuilder<int>(cube.Schema
        , new DimensionReferenceTranslator<int>(cube.Schema),
        new MeasureReferenceTranslator<int>(cube.Schema));

      builder.Define(b =>
        b.Or(x => x.Dimension("category").IsEquals("clothes"),
        x => x.Dimension("category").IsEquals("shoes")));

      var predicate = builder.Build();

      Assert.IsNotNull(predicate);
      Assert.IsTrue(predicate.FiltersOnAggregation());
    }

    [Test]
    public void WhereBuilder_Add_And_Expression_With_DimensionSlicers_And_MeasureSlicer_Test()
    {
      WhereBuilder<int> builder = new WhereBuilder<int>(cube.Schema
        , new DimensionReferenceTranslator<int>(cube.Schema),
        new MeasureReferenceTranslator<int>(cube.Schema));

      builder.Define(b =>
        b.And(x => x.Dimension("category").IsEquals("clothes"),
        x => x.Measure("quantity").GreaterOrEquals(2))
      );

      var predicate = builder.Build();

      Assert.IsNotNull(predicate);
      Assert.IsTrue(predicate.FiltersOnAggregation());
      Assert.IsTrue(predicate.FiltersOnFacts());
    }

    [Test]
    public void QueryBuilder_Create_Test()
    {
      var query = cube.BuildQuery()
        .OnRows("sex.male")
        .OnColumns("category.shoes")
        .AddMeasures("quantity");

      var result = query.Create();

      Assert.IsNotNull(result);
    }

    [Test]
    public void QueryBuilder_With_Where_Create_Test()
    {
      var query = cube.BuildQuery()
        .OnRows("sex.male")
        .OnColumns("category.shoes")
        .AddMeasures("quantity")
        .Where(b => b.Define(x => x.Measure("spent").GreaterOrEquals(100))) ;

      var result = query.Create();

      Assert.IsNotNull(result);
    }

    [Test]
    public void QueryBuilder_With_Where_Create_2_Test()
    {
      var query = cube.BuildQuery()
        .OnRows("sex.male", "sex.female")
        .OnColumns("category.shoes")
        .AddMeasures("quantity")
        .Where(b => b.Define(x => x.Measure("spent").GreaterOrEquals(100)));

      var result = query.Create();

      Assert.IsNotNull(result);
    }

    [Test]
    public void QueryBuilder_With_Where_Create_3_Test()
    {
      var query = cube.BuildQuery()
        .OnRows("sex.male", "sex.female")
        .OnColumns("category.shoes", "category.toys")
        .AddMeasures("quantity")
        .Where(b => b.Define(x => x.Measure("spent").GreaterOrEquals(100)));

      var result = query.Create();

      Assert.IsNotNull(result);
    }

    [Test]
    public void QueryBuilder_With_Where_Create_4_Test()
    {
      var query = cube.BuildQuery()
        .OnRows("sex.male", "sex.female")
        .OnColumns("category.shoes", "category.toys")
        .AddMeasures("quantity", "spent")
        .Where(b => b.Define(x => x.Measure("spent").GreaterOrEquals(100)));

      var result = query.Create();

      Assert.IsNotNull(result);
    }
  }
}