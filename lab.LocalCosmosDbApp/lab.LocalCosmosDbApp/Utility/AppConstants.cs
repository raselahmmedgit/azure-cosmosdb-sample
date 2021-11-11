﻿namespace lab.LocalCosmosDbApp.Utility
{
    public class AppConstants
    {
        public static string Base64ImagePrefix = "data:image/png;base64,";
        public static string Base64DefaultPhotoImage = "data:image/png;base64,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";
        public static string Base64DefaultImage = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAMoAAADKCAYAAADkZd+oAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAArcSURBVHhe7d1PTJvJGcfx8UsUKFH5s9sER0oCZLONI2hB3WpB6r2CG9zILdygJ3LvVrR7SW9IvXDIqhyRcghSKkEPkaIeWnPY1qikeJtsYJPQmERdHNREUDVL32f8Dklsv+OxeSE2+X6kR/bYvMFgfnln3hm/b0wFZmZmfrOzs/OL9fX11s7OTm94eDh4ptD29raanp5W2Ww2eCSnr69PDQ4OBq1CmUxGvo/e/k1DQ0Oqt7c3aBVaW1tTs7Ozb23X0NCgRkZGVEdHR/BIoVQqpebm5oJWjmx35coVFY/Hg0cKzc/Pq8XFxaCV09LSosbGxvT2YW7evKmWlpaCVo58H9nORn6X8rt5UyKR0D9fmErfA/l62S6K90DIa5TXGqbS9yCZTKqFhYWglVPpeyDbTUxMBK3i8t+D8+fPZ5uamn7nv85fSdvb3d2tu3Hjxh/8H+izlZWVD7e2tjz9lSX42wX3yhPVdu/6+7ty2a7Y11S6nYvD3i5fNb7u/K958OBBy+rq6mebm5uf+8/Vxa5fv/7L5eXlz+XJgYEB/b+DJBB4X8leM51O6xy0t7d/5z/089jt27f/duvWrd5Su17gfeXvUf4Y83ctmVgs1hY8BiCPH5SNmD+4i6bjCRxhTgN34H1HUAAHBAVwQFAAC5mglWIwD1hcu3ZN37JHASxk8lHKm5qaUlIAwnmmDwYgHF0vwAFBASxkobAUQQEs+vv7dXnyQR8pAOGYRwEc0PUCHBAUwAFBARwQFMABg3nAwpzCiKAAFnurh1nrBYTbWz0sZ8iTAhDOM4kBEI6jXoADggJYyEnIpQgKYCHn4paK3blzRx8e5rzDQDjmUQAHdL0ABwQFcEBQAAcEBXDAYB6wkEvUCYICWExOTupbTy6LLAUgnCfXDpcCEI7Vw4ADjnoBFvX19boICmAxOjqqi6AAFvF4XFcsmUzqw8OylBhAccyjAA7oegEOCArggKAADggK4IDBPGCRTCb1LXsUwGJhYUGXJ+vtzZp7AMWxehhwQNcLcEBQAAtWDwMOxsfHdXkmMQAKtbS06Iql02k9jyJLiQEUx4Qj4IAxCuCAoAAOCArggKAADhjMAxayIFKwRwEsZJm9lGfuAAjnmfX2AMLR9QIcEBTAorm5WRdBASyuXr2qS68elsQACBfLZDJ6HqWhoUE/ALtXr/6nnj3dUBsbGfXy5Yvg0dpQV1enPvjgB+pUW1w1NfGfYzmYcCzDt9/+W92/l/bD8ip4pHadPNmmOs9/5IfnWPAIbBijOJKQfJW+eyRCIp4921DplbtBC6UQFAfS3ZI9yVGztfVcPXrIhW5d0PVyIH9Mjx8/DFqvNTaeUHXHaqfr8vLFfwr2iDJu+bTvZ0ELYQiKg6XUlwUD9wsXLqqTp9qCVm2QPePy35cKfpau7h4G9yXQ9XKQ/4cle5JaC4mQgfvZcx1B67Xn2c3gHvKZE0QSlArUUncr37Eafu3vgjnlsDc3N6ekAITzUqmUkkJ05FDyo0ff6Hr6NKPHBqhtdL0iJIG4u7yk51se+yGR+vr+P3MHA17U1iw+3kZQIpRO/0PPTeTb2dlRy8sp9iw1qL29XRdBiYh0t7aeZ4NWIZm/ePKv9aCFWjE6OqrLM+vtsT8ymVfK8yJ7G9QGz6y3BxCOrldE6utLf0yh4YCuGiBjn8yTdX+MdFd3ARE9ghIRmakvFZYzZ9uDe9Exy1JWV79Wm8EK5/v3vgqeRVQISoQSia7QmW9ZG+ay1ymHCUn+EhtZQk9YokVQItR44oT6ySefqrP+nqOpuUXXKX9PI49FvTYsLCQGYYkWQYmYLDyULlZX1491fXSIe5J8hCU6BKXGuIbEICz7MzU1pYug1BBbSGTpv3yuRD6IlY+wVC6bzeryzHp7VLdSIen+Ue7DV93dvYTlAHhmvT2ql0tIzNlU5ICCLSyrD+4HLZSDrleVKyckhi0srmMbvI2gVLFKQmLYwgJ3PT09ugjKAZLPoPz1y0X1lz//qezPpOwnJAZh2b/h4WFd3sWLF5UUoiWhkM+gyGdRdNv/g5e2S1iiCIlBWKLhXb58WUkhOiYk+efQknapsEQZEoOw7B9dr4iFhcSwheUgQmIQlv0hKBEqFRKjWFgOMiSGhOXCx4mghXIQlIjYQnL8eOHnUN4My2GExOC8XpUhKBGwheTkyVPqk5/26dt8JiyHFRJUjqDsU6mQmK6O3IaFhZBUr+3tbV0EZR9cQ2KEhSUfIake09PTughKhcoNiVEqLISkuuytHjbr7eHuvzvbFYXEsIVlx/+3d7Zzk5SoHp5JDNzJbHulITHCwiL/roRQ9lioHnS9IlJOSAzCUjsISgQqCYlBWKrbwMCALoKyT/sJiUFYqld/f78ur6+vT0mhfFGExCAs1c0bHBxUUihPlCExbGG5dwQv311L6HpVQOY6og6JERaWYrP3ODwEpQIHfbHTYmEptrASh4egVCkJy5kz59T3m5pVa+uH6tKl7uAZvAsEpYrJNeG7u3tU4lKX/iwJ3h2CAlhkMhldBAWwmJmZ0cVaL8Bi7/MoZr09wuWfkEEubCof361Fz7Obwb3XGhq+F9xDmNjExMSu3JmcnNQPoND9e2n17NnToJUjcynn/MH2QR8qjpJcA//Rw7Wg9Zpc6Cjqa7gcFSYXBMWB/IHdXV4KWkeLHH6WI2sozuSCwbwDuZyCzGUcNdKlPN95IWihmJGREV0ExdHHP7yo//c9KiQknX5ImJ+xSyQSumILCwu66yVLiVGa9PGfPFnXCxVrlQRe9iSExF0sm83qoKA8Mm4pdgSpmsnRrabmZgbuFSAogAPGKIADggI4ICiAA8YogIW5YjZBASz2ZubNensA4Tyz3h5AOM+stwcQjqNegEV9fb0uggJYmMvLExTAoqOjQ1csmUzqw8OylBhAccyjAA7oegEOCArggKAADggK4IDBPGCRSqX0LUEBLPZWD8t6e7PmHkBx3uzsrJICEI7BPOCAoAAWrB4GHIyPj+vyTGIAFGppadEVS6fT+vBwPB7XTwAoxDwK4IAxCuCAoAAOCArggKAADhjMAxYLCwv6lj0KYJFMJnV55g6AcJ7sWszuBUBxdL0ABwQFsGhubtZFUAALVg8DDhoaGnTF1tbW9DyKLCUGUBwTjoADxiiAA4ICOCAogAOCAjhgMA9YmJNDskcBLMwph1kUCThgmT3ggK4X4ICgABZtbW26CApgsbd62Ky3BxCOeRTAAV0vwAFBARwQFMABQQEcEBTAAUe9AIvp6Wl9yx4FsMhkMro8WW9v1twDKM4z6+0BhKPrBTggKIBFT0+PLoICWAwPD+vyjh8/rg8Pb29v6ycAvG13d3fDi8fjz6TBgB4IlfJOnz79W7m3srKiHwHwmr83+c6/uRaTRjKZ/H1HR8cVOb19mPn5ebW4uBi0cuQM+BMTE0GrOJmjyd9bJRIJNTIyErSKm5qaUtlsNmjlyKBK+othpPsoM6n52w0MDKj+/v6gVUgmlGZmZgq6n/Ia5bWGkZ8rfw5Kfoeynf/7DB4plEql1NzcXNDKke3GxsasVxWI8j3wexL6+9nI71J+N2867PdgaGhI9fb2Bq1Ca2tr+ud7c7so3wM/KL9ubW2d1IN5/wcYbWxs7PLvfuE/8Y08li//BxD+1wb3whX7mkq3c3HY2+Wrxtdd7Gsq3c7Fu95uv/+OfysZ+MLzvC4JiVJK/R8lq+wC9yMA9gAAAABJRU5ErkJggg==";
        public static string DefaultPhotoImagePath = "/default/images/default-photo-image.png";
        public static string DefaultImagePath = "/default/images/default-image.png";
        public static string DefaultLogoPath = "default/images/default-image.png";
        public static string[] AllowedUploadedFileTypes = { "image/png", "image/jpg", "image/jpeg", "image/png" };
        public static string BaseUrl = "";
        public static bool IsDevelopmentMode = true;

        public static int PageSize = 10;
        public static int Page = 1;

        public static class AppUserRole
        {
            public static string Admin = "Admin";
            public static string Member = "Member";
        }

    }

}
