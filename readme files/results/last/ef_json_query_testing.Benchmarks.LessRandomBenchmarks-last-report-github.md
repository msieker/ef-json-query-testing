``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1766 (21H1/May2021Update)
AMD Ryzen 7 1700, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.300
  [Host]     : .NET 6.0.5 (6.0.522.21309), X64 RyuJIT
  DefaultJob : .NET 6.0.5 (6.0.522.21309), X64 RyuJIT


```
|                         Method |                 Categories |         Mean |      Error |     StdDev |
|------------------------------- |--------------------------- |-------------:|-----------:|-----------:|
|     Indexed_Last_both_int_bool | json,lessrand,indexed,last |     4.833 ms |  0.0956 ms |  0.1624 ms |
|       Media_last_both_int_bool |  table,lessrand,media,last |   153.960 ms |  1.8422 ms |  1.7232 ms |
|      Indexed_Last_req_int_bool | json,lessrand,indexed,last |     8.191 ms |  0.1634 ms |  0.3622 ms |
|        Media_last_req_int_bool |  table,lessrand,media,last |   174.140 ms |  2.8460 ms |  2.3765 ms |
|        Indexed_Last_req_string | json,lessrand,indexed,last | 1,454.150 ms | 13.6095 ms | 12.7303 ms |
|          Media_last_req_string |  table,lessrand,media,last | 3,151.460 ms | 54.4148 ms | 50.8997 ms |
| Indexed_Last_req_string_single | json,lessrand,indexed,last | 1,365.279 ms | 13.8439 ms | 12.2722 ms |
|   Media_last_req_string_single |  table,lessrand,media,last |   830.242 ms |  6.4290 ms |  5.3685 ms |
