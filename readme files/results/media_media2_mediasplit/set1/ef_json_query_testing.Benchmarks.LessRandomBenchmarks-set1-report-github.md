``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1766 (21H1/May2021Update)
AMD Ryzen 7 1700, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.300
  [Host]     : .NET 6.0.5 (6.0.522.21309), X64 RyuJIT
  DefaultJob : .NET 6.0.5 (6.0.522.21309), X64 RyuJIT


```
|                            Method |                          Categories |         Mean |      Error |       StdDev |
|---------------------------------- |------------------------------------ |-------------:|-----------:|-------------:|
|          Media_set1_both_int_bool |           table,lessrand,media,set1 |    142.64 ms |   1.492 ms |     1.395 ms |
|         Media2_set1_both_int_bool |          table,lessrand,media2,set1 |    138.20 ms |   2.140 ms |     2.002 ms |
|     MediaSplit_set1_both_int_bool |      table,lessrand,mediasplit,set1 |    269.63 ms |   2.591 ms |     2.023 ms |
|           Media_set1_req_int_bool |           table,lessrand,media,set1 |    271.14 ms |   2.829 ms |     2.508 ms |
|          Media2_set1_req_int_bool |          table,lessrand,media2,set1 |    252.33 ms |   4.128 ms |     3.447 ms |
|      MediaSplit_set1_req_int_bool |      table,lessrand,mediasplit,set1 |    514.98 ms |   7.650 ms |     6.388 ms |
|                 Media_set1_op_int |           table,lessrand,media,set1 |     72.03 ms |   1.428 ms |     1.466 ms |
|                Media2_set1_op_int |          table,lessrand,media2,set1 |     71.51 ms |   1.417 ms |     2.519 ms |
|            MediaSplit_set1_op_int |      table,lessrand,mediasplit,set1 |    133.33 ms |   1.058 ms |     0.938 ms |
|             Media_set1_req_string |           table,lessrand,media,set1 |    165.93 ms |   3.305 ms |     6.288 ms |
|            Media2_set1_req_string |          table,lessrand,media2,set1 |    273.95 ms |   5.425 ms |    11.792 ms |
|        MediaSplit_set1_req_string |      table,lessrand,mediasplit,set1 |  1,706.35 ms |  25.114 ms |    23.492 ms |
|       Media_set1_op_string_single |      table,lessrand,media,set1,miss | 20,429.51 ms | 614.136 ms | 1,771.924 ms |
|      Media2_set1_op_string_single |     table,lessrand,media2,set1,miss |     77.70 ms |   1.695 ms |     4.837 ms |
|  MediaSplit_set1_op_string_single | table,lessrand,mediasplit,set1,miss |    506.94 ms |   7.110 ms |     6.650 ms |
|      Media_set1_req_string_single |      table,lessrand,media,set1,miss | 29,390.85 ms | 580.884 ms | 1,346.287 ms |
|     Media2_set1_req_string_single |     table,lessrand,media2,set1,miss |    141.08 ms |   2.803 ms |     6.441 ms |
| MediaSplit_set1_req_string_single | table,lessrand,mediasplit,set1,miss |    984.41 ms |   4.539 ms |     4.024 ms |
