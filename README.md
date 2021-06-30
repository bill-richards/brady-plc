# brady-plc

## The Solution

The steps taken to create the solution are documented [here](./generation-report-compiler/README.md)

## The Task

You are required to provide a production ready solution (Console Application) written in C#.

An XML file (01-Basic.xml) containing energy generator data is provided in a (configurable) directory on a regular basis.

The solution should:

1. *automatically pick up the received XML file as soon as it is placed in the input folder*
2. parse and process the contained data 

in order to achieve the following:

1. Output the Total Generation Value for *each* generator.
2. Determine which generator has the highest Daily Emissions for each day, and output the emission value.
3. Output the Actual Heat Rate for *each* coal generator.

> The location of the input folder is set in a configuration file

The output should be contained within a single XML file in the format as specified by an example accompanying file 01-Basic-Result.xml.

> The location of the output folder is configured in the Application app.config file.

## Notes

- Example files supplied input/output: 01-Basic.xml -> 01-Basic-Result.xml
- There are no XSDs provided for input/output XMLs
- Emissions only apply to fossil fuel generator types e.g. coal and gas
- There could be a varying number of generators of a given type in any one GenerationReport

## Calculation Definitions and Reference Data

**Daily Generation Value** = *Energy x Price x ValueFactor*

**Daily Emissions** = *Energy x EmissionRating x EmissionFactor*

**Actual Heat Rate** = *TotalHeatInput / ActualNetGeneration*

***ValueFactor*** and ***EmissionsFactor*** are static data sourced from the accompanying *ReferenceData.xml* file.

> **Note**: it is not possible to change static data while the console application is running.

Generator Types map to factors as follows:

| Generator Type | ValueFactor | EmissionFactor |
|---|---|---|
Offshore Wind | Low | N/A
Onshore Wind | High | N/A
Gas | Medium | Medium
Coal| Medium | High
