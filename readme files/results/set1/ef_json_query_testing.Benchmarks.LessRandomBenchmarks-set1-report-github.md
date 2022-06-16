``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1766 (21H1/May2021Update)
AMD Ryzen 7 1700, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.300
  [Host]     : .NET 6.0.5 (6.0.522.21309), X64 RyuJIT
  DefaultJob : .NET 6.0.5 (6.0.522.21309), X64 RyuJIT


```
|                         Method |                     Categories |         Mean |      Error |     StdDev |
|------------------------------- |------------------------------- |-------------:|-----------:|-----------:|
|     Indexed_set1_both_int_bool |     json,lessrand,indexed,set1 |    12.407 ms |  0.2388 ms |  0.3348 ms |
|       Media_set1_both_int_bool |      table,lessrand,media,set1 |   137.246 ms |  1.3075 ms |  1.2230 ms |
|      Indexed_set1_req_int_bool |     json,lessrand,indexed,set1 |    17.118 ms |  0.3411 ms |  0.8106 ms |
|        Media_set1_req_int_bool |      table,lessrand,media,set1 |   272.267 ms |  1.5866 ms |  1.4065 ms |
|            Indexed_set1_op_int |     json,lessrand,indexed,set1 |     7.484 ms |  0.1443 ms |  0.1604 ms |
|              Media_set1_op_int |      table,lessrand,media,set1 |    68.615 ms |  1.3577 ms |  2.1924 ms |
|        Indexed_set1_req_string |     json,lessrand,indexed,set1 |   414.619 ms |  2.1831 ms |  1.7044 ms |
|          Media_set1_req_string |      table,lessrand,media,set1 | 1,550.519 ms | 30.6661 ms | 30.1182 ms |
|  Indexed_set1_op_string_single |     json,lessrand,indexed,set1 | 2,527.856 ms | 45.7465 ms | 42.7913 ms |
|    Media_set1_op_string_single | table,lessrand,media,set1,miss |   424.815 ms |  0.9755 ms |  0.7616 ms |
| Indexed_set1_req_string_single |     json,lessrand,indexed,set1 | 1,406.252 ms | 24.1559 ms | 22.5955 ms |
|   Media_set1_req_string_single | table,lessrand,media,set1,miss |   851.747 ms |  7.7749 ms |  6.4924 ms |
