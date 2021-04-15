# NSimpleOLAP 

The goal of this project is to build an embeddable .Net **OLAP** library that can be used within the context of console, desktop, or other types of applications.

This was also being developed for educational purposes, as to educate more developers on the utility of aggregation engines beyond the field of Business Intelligence and Finance.

At the present moment this project is still in alpha stage and unstable, it allows for some basic querying and modes of aggregation.
More on that later.

## Quick Start

Building a Cube will require some intial setup to identify the data sources, mappings and define the metadata.
In the following example we will build a Cube from data that is contained in CSV files, and these will be used to define the Cube dimensions and measure.


```csharp
CubeBuilder builder = new CubeBuilder();

builder.SetName("Hello World")
.SetSourceMappings((sourcebuild) =>
{
  sourcebuild.SetSource("sales")
  .AddMapping("category", "category")
  .AddMapping("sex", "sex"));
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
``` 


Creating the Cube and processing the data will be done as follows.


```csharp

var cube = builder.Create<int>();

cube.Initialize();
cube.Process();

``` 


Querying the Cube will be done by using the querying interface, here's a basic example:


```csharp

var queryBuilder = cube.BuildQuery()
  .OnRows("sex.female")
  .OnColumns("category.shoes")
  .AddMeasures("quantity");

var query = queryBuilder.Create();
var result = query.StreamCells().ToList();

``` 