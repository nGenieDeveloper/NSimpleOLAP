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
  public class QueryExecutionTests
  {
    private Cube<int> cube;

    public QueryExecutionTests()
    {
      Init();
    }

    public void Init()
    {
      cube = CubeSourcesFixture.GetBasicCubeThreeDimensionsTwoMeasures2();
      cube.Initialize();
      cube.Process();
    }

    [OneTimeTearDown]
    public void Dispose()
    {
      cube.Dispose();
    }

    [Test]
    public void Query_Run_With_Single_Cell_Test()
    {
      var queryBuilder = cube.BuildQuery()
        .OnRows("sex.female")
        .OnColumns("category.shoes")
        .AddMeasures("quantity");

      var query = queryBuilder.Create();
      var result = query.Run().ToList();

      Assert.IsTrue(result.Count == 1);
    }

    [Test]
    public void Query_Run_With_2_Cells_Test()
    {
      var queryBuilder = cube.BuildQuery()
        .OnRows("sex.female", "sex.male")
        .OnColumns("category.shoes")
        .AddMeasures("quantity");

      var query = queryBuilder.Create();
      var result = query.Run().ToList();

      Assert.IsTrue(result.Count == 2);
    }

    [Test]
    public void Query_Run_With_All_Cells_Test()
    {
      var queryBuilder = cube.BuildQuery()
        .OnRows("sex.All")
        .OnColumns("category.shoes")
        .AddMeasures("quantity");

      var query = queryBuilder.Create();
      var result = query.Run().ToList();

      Assert.IsTrue(result.Count == 3);
    }

    [Test]
    public void Query_Run_With_All_Cells_With_Extra_Dims_Test()
    {
      var queryBuilder = cube.BuildQuery()
        .OnRows("category.All.place.Paris")
        .OnColumns("sex.male")
        .AddMeasures("quantity");

      var query = queryBuilder.Create();
      var result = query.Run().ToList();

      Assert.IsTrue(result.Count == 2);
    }
  }
}
