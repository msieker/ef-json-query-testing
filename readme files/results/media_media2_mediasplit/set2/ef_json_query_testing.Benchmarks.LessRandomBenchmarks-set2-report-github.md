``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1766 (21H1/May2021Update)
AMD Ryzen 7 1700, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.300
  [Host]     : .NET 6.0.5 (6.0.522.21309), X64 RyuJIT
  DefaultJob : .NET 6.0.5 (6.0.522.21309), X64 RyuJIT


```
|                            Method |                     Categories |       Mean |    Error |   StdDev |
|---------------------------------- |------------------------------- |-----------:|---------:|---------:|
|          Media_set2_both_int_bool |      table,lessrand,media,set2 |   221.1 ms |  2.18 ms |  1.82 ms |
|         Media2_set2_both_int_bool |     table,lessrand,media2,set2 |   206.7 ms |  3.15 ms |  3.24 ms |
|     MediaSplit_set2_both_int_bool | table,lessrand,mediasplit,set2 |   415.4 ms |  4.85 ms |  4.30 ms |
|                Media_set2_req_int |      table,lessrand,media,set2 |   172.6 ms |  3.14 ms |  2.93 ms |
|               Media2_set2_req_int |     table,lessrand,media2,set2 |   171.7 ms |  3.06 ms |  2.87 ms |
|           MediaSplit_set2_req_int | table,lessrand,mediasplit,set2 |   335.1 ms |  2.44 ms |  2.16 ms |
|             Media_set2_req_string |      table,lessrand,media,set2 |   169.9 ms |  3.34 ms |  6.52 ms |
|            Media2_set2_req_string |     table,lessrand,media2,set2 |   267.0 ms |  4.74 ms |  6.33 ms |
|        MediaSplit_set2_req_string | table,lessrand,mediasplit,set2 | 1,711.8 ms | 12.88 ms | 12.05 ms |
|      Media_set2_req_string_single |      table,lessrand,media,set2 |   250.6 ms |  3.89 ms |  5.19 ms |
|     Media2_set2_req_string_single |     table,lessrand,media2,set2 |   131.5 ms |  2.42 ms |  3.92 ms |
| MediaSplit_set2_req_string_single | table,lessrand,mediasplit,set2 |   960.7 ms | 11.23 ms | 10.51 ms |
