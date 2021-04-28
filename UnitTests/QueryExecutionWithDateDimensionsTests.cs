using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSimpleOLAP;
using NSimpleOLAP.Common;
using NSimpleOLAP.Query;
using NSimpleOLAP.Query.Builder;
using NUnit.Framework;
using NSimpleOLAP.Renderers;

namespace UnitTests
{
  [TestFixture]
  public class QueryExecutionWithDateDimensionsTests
  {
    private Cube<int> cube;

    public QueryExecutionWithDateDimensionsTests()
    {
      Init();
    }

    public void Init()
    {
      cube = CubeSourcesFixture.GetBasicCubeThreeSimpleDimensionsTwoMeasuresAndThreeDateDimensions();
      cube.Initialize();
      cube.Process();
    }

    [OneTimeTearDown]
    public void Dispose()
    {
      cube.Dispose();
    }

    [Test]
    public void Simple_Query_Month_Year_On_Measure_Quantity()
    {
      var queryBuilder = cube.BuildQuery()
        .OnRows("Month.All")
        .OnColumns("Year.All")
        .AddMeasuresOrMetrics("quantity");

      var query = queryBuilder.Create();
      var result = query.StreamRows().ToList();

      Assert.IsTrue(result[0].Length == 2);
      Assert.IsTrue(result.Count == 13);

      // output for checking, temporary
      result.RenderInConsole();
    }
  }
}
