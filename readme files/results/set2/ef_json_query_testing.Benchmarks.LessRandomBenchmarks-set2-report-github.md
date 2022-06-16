``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1766 (21H1/May2021Update)
AMD Ryzen 7 1700, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.300
  [Host]     : .NET 6.0.5 (6.0.522.21309), X64 RyuJIT
  DefaultJob : .NET 6.0.5 (6.0.522.21309), X64 RyuJIT


```
|                         Method |                 Categories |         Mean |      Error |     StdDev |
|------------------------------- |--------------------------- |-------------:|-----------:|-----------:|
|     Indexed_set2_both_int_bool | json,lessrand,indexed,set2 |     7.835 ms |  0.1562 ms |  0.3620 ms |
|       Media_set2_both_int_bool |  table,lessrand,media,set2 |   223.547 ms |  1.5846 ms |  1.5563 ms |
|           Indexed_set2_req_int | json,lessrand,indexed,set2 |     7.865 ms |  0.1430 ms |  0.1117 ms |
|             Media_set2_req_int |  table,lessrand,media,set2 |   174.690 ms |  2.6236 ms |  2.9162 ms |
|        Indexed_set2_req_string | json,lessrand,indexed,set2 |   410.363 ms |  5.4823 ms |  4.5780 ms |
|          Media_set2_req_string |  table,lessrand,media,set2 | 1,550.718 ms | 25.9598 ms | 24.2828 ms |
| Indexed_set2_req_string_single | json,lessrand,indexed,set2 | 1,370.598 ms | 22.5360 ms | 21.0802 ms |
|   Media_set2_req_string_single |  table,lessrand,media,set2 |   831.704 ms | 12.5529 ms | 11.7420 ms |
