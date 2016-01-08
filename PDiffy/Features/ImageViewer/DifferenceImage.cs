﻿using System.Linq;
using System.Threading.Tasks;
using MediatR;
using PDiffy.Data.Stores;
using PDiffy.Infrastructure;
using Quarks.ImageExtensions;

namespace PDiffy.Features.ImageViewer
{
	public class DifferenceImage
	{
		public class Query : IAsyncRequest<Result>
		{
			public string Name { get; set; }
		}

		public class Result
		{
			public byte[] ImageData { get; set; }
		}

		public class Handler : IAsyncRequestHandler<Query, Result>
		{
			readonly IImageStore _imageStore;

			public Handler(IImageStore imageStore)
			{
				_imageStore = imageStore;
			}

			public async Task<Result> Handle(Query message)
			{
				var page = Data.Biggy.ImageComparisons.Single(x => x.Name == message.Name);

				if (string.IsNullOrWhiteSpace(page.DifferenceImagePath))
				{
					page.DifferenceImagePath = _imageStore.Save(page.DifferenceImage, page.Name, Environment.DifferenceId);
					Data.Biggy.ImageComparisons.Update(page);
				}

				return new Result { ImageData = page.DifferenceImage.ToByteArray()};
			}
		}
	}
}