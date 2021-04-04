using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSimpleOLAP;
using NSimpleOLAP.Common;
using NSimpleOLAP.Configuration.Fluent;
using NSimpleOLAP.Query;
using NSimpleOLAP.Query.Builder;
using NUnit.Framework;


namespace UnitTests
{
  [TestFixture]
  public class SchemaTranslatorsTests
  {
    private Cube<int> cube;
    public SchemaTranslatorsTests()
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
    public void Dimension_Translator_GetDimension_Test()
    {
      var translator = new DimensionReferenceTranslator<int>(cube.Schema);

      var result = translator.GetDimension("category");

      Assert.AreEqual(1, result);
    }

    [Test]
    public void Dimension_Translator_GetDimensionMember_Test()
    {
      var translator = new DimensionReferenceTranslator<int>(cube.Schema);
      var dimKey = translator.GetDimension("category");
      var result = translator.GetDimensionMember(dimKey, "shoes");

      Assert.AreEqual(4, result);
    }

    [Test]
    public void Dimension_Translator_Translate_Simple_Test()
    {
      var translator = new DimensionReferenceTranslator<int>(cube.Schema);

      var result = translator.Translate("category.shoes");

      Assert.IsTrue(result.Length > 0);
      Assert.AreEqual(new KeyValuePair<int,int>(1,4), result[0]);
    }

    [Test]
    public void Dimension_Translator_Translate_Two_Dims_Test()
    {
      var translator = new DimensionReferenceTranslator<int>(cube.Schema);

      var result = translator.Translate("sex.male.category.shoes");

      Assert.IsTrue(result.Length > 1);
      Assert.AreEqual(new KeyValuePair<int, int>(2, 1), result[0]);
      Assert.AreEqual(new KeyValuePair<int, int>(1, 4), result[1]);
    }
  }
}
