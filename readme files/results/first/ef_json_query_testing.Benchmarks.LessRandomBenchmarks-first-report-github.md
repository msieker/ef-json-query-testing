``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1766 (21H1/May2021Update)
AMD Ryzen 7 1700, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.300
  [Host]     : .NET 6.0.5 (6.0.522.21309), X64 RyuJIT
  DefaultJob : .NET 6.0.5 (6.0.522.21309), X64 RyuJIT


```
|                          Method |                  Categories |         Mean |      Error |     StdDev |
|-------------------------------- |---------------------------- |-------------:|-----------:|-----------:|
|     Indexed_first_both_bool_int | json,lessrand,indexed,first |     7.008 ms |  0.1370 ms |  0.3773 ms |
|       Media_first_both_bool_int |  table,lessrand,media,first |   138.727 ms |  2.2613 ms |  1.7655 ms |
|      Indexed_first_req_bool_int | json,lessrand,indexed,first |    27.680 ms |  0.4322 ms |  0.4625 ms |
|        Media_first_req_bool_int |  table,lessrand,media,first |   267.641 ms |  3.9830 ms |  3.3260 ms |
|            Indexed_first_op_int | json,lessrand,indexed,first |     5.210 ms |  0.1038 ms |  0.2097 ms |
|              Media_first_op_int |  table,lessrand,media,first |    90.174 ms |  1.2907 ms |  1.0778 ms |
|        Indexed_first_req_string | json,lessrand,indexed,first | 1,470.281 ms | 22.9791 ms | 21.4946 ms |
|          Media_first_req_string |  table,lessrand,media,first | 3,155.082 ms | 26.4490 ms | 22.0861 ms |
|         Indexed_first_op_string | json,lessrand,indexed,first | 2,498.910 ms | 20.6448 ms | 18.3011 ms |
|           Media_first_op_string |  table,lessrand,media,first |   492.755 ms |  7.3617 ms |  6.8861 ms |
|  Indexed_first_op_string_single | json,lessrand,indexed,first | 2,478.082 ms | 14.1720 ms | 13.2565 ms |
|    Media_first_op_string_single |  table,lessrand,media,first |   402.723 ms |  1.2851 ms |  1.0033 ms |
| Indexed_first_req_string_single | json,lessrand,indexed,first | 1,387.691 ms | 13.4742 ms | 11.9446 ms |
|   Media_first_req_string_single |  table,lessrand,media,first |   824.639 ms |  6.2622 ms |  5.2292 ms |
