``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1766 (21H1/May2021Update)
AMD Ryzen 7 1700, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.300
  [Host]     : .NET 6.0.5 (6.0.522.21309), X64 RyuJIT
  DefaultJob : .NET 6.0.5 (6.0.522.21309), X64 RyuJIT


```
|                             Method |                       Categories |        Mean |     Error |    StdDev |
|----------------------------------- |--------------------------------- |------------:|----------:|----------:|
|          Media_first_both_bool_int |       table,lessrand,media,first |   138.55 ms |  1.398 ms |  1.240 ms |
|         Media2_first_both_bool_int |      table,lessrand,media2,first |   131.21 ms |  2.565 ms |  2.399 ms |
|     MediaSplit_first_both_bool_int |  table,lessrand,mediasplit,first |   268.67 ms |  3.958 ms |  4.235 ms |
|           Media_first_req_bool_int |       table,lessrand,media,first |   244.19 ms |  3.443 ms |  3.052 ms |
|          Media2_first_req_bool_int |      table,lessrand,media2,first |   261.12 ms |  4.572 ms |  4.892 ms |
|      MediaSplit_first_req_bool_int | table,lessrand,mediapsplit,first |   494.69 ms |  5.648 ms |  5.007 ms |
|                 Media_first_op_int |       table,lessrand,media,first |    90.29 ms |  1.455 ms |  1.361 ms |
|                Media2_first_op_int |      table,lessrand,media2,first |    89.69 ms |  1.775 ms |  2.602 ms |
|            MediaSplit_first_op_int |  table,lessrand,mediasplit,first |   174.71 ms |  1.822 ms |  1.615 ms |
|             Media_first_req_string |       table,lessrand,media,first | 3,126.07 ms | 28.634 ms | 25.384 ms |
|            Media2_first_req_string |      table,lessrand,media2,first | 3,087.45 ms | 17.777 ms | 14.844 ms |
|        MediaSplit_first_req_string |  table,lessrand,mediasplit,first | 6,230.33 ms | 32.916 ms | 32.327 ms |
|              Media_first_op_string |       table,lessrand,media,first |   490.59 ms |  3.968 ms |  3.518 ms |
|             Media2_first_op_string |      table,lessrand,media2,first |   471.33 ms |  2.087 ms |  1.743 ms |
|         MediaSplit_first_op_string |  table,lessrand,mediasplit,first |   977.16 ms | 10.736 ms |  8.965 ms |
|       Media_first_op_string_single |       table,lessrand,media,first |    76.54 ms |  1.812 ms |  5.286 ms |
|      Media2_first_op_string_single |      table,lessrand,media2,first |    72.76 ms |  1.797 ms |  5.242 ms |
|  MediaSplit_first_op_string_single |  table,lessrand,mediasplit,first |   481.82 ms |  9.060 ms |  8.475 ms |
|      Media_first_req_string_single |       table,lessrand,media,first |   139.49 ms |  2.745 ms |  6.082 ms |
|     Media2_first_req_string_single |      table,lessrand,media2,first |   137.81 ms |  2.750 ms |  5.978 ms |
| MediaSplit_first_req_string_single |  table,lessrand,mediasplit,first |   964.29 ms | 13.628 ms | 12.748 ms |
