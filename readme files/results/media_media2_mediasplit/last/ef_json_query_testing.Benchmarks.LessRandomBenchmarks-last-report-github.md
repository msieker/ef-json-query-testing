``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1766 (21H1/May2021Update)
AMD Ryzen 7 1700, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.300
  [Host]     : .NET 6.0.5 (6.0.522.21309), X64 RyuJIT
  DefaultJob : .NET 6.0.5 (6.0.522.21309), X64 RyuJIT


```
|                            Method |                     Categories |        Mean |     Error |    StdDev |
|---------------------------------- |------------------------------- |------------:|----------:|----------:|
|          Media_last_both_int_bool |      table,lessrand,media,last |    91.75 ms |  1.076 ms |  0.954 ms |
|         Media2_last_both_int_bool |     table,lessrand,media2,last |    90.21 ms |  0.868 ms |  0.852 ms |
|     MediaSplit_last_both_int_bool | table,lessrand,mediasplit,last |   298.77 ms |  1.814 ms |  1.416 ms |
|           Media_last_req_int_bool |      table,lessrand,media,last |   176.73 ms |  1.719 ms |  1.342 ms |
|          Media2_last_req_int_bool |     table,lessrand,media2,last |   168.60 ms |  3.338 ms |  3.710 ms |
|      MediaSplit_last_req_int_bool | table,lessrand,mediasplit,last |   344.53 ms |  6.879 ms |  5.744 ms |
|             Media_last_req_string |      table,lessrand,media,last | 3,088.20 ms | 17.647 ms | 15.644 ms |
|            Media2_last_req_string |     table,lessrand,media2,last | 3,082.23 ms | 14.031 ms | 11.717 ms |
|        MediaSplit_last_req_string | table,lessrand,mediasplit,last | 6,190.43 ms | 27.477 ms | 25.702 ms |
|      Media_last_req_string_single |      table,lessrand,media,last |   262.43 ms | 12.492 ms | 31.797 ms |
|     Media2_last_req_string_single |     table,lessrand,media2,last |   135.50 ms |  2.703 ms |  7.842 ms |
| MediaSplit_last_req_string_single | table,lessrand,mediasplit,last |   967.50 ms |  8.919 ms |  6.964 ms |
