using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Services.Utilities
{
	public class DocumentSettings
	{
		public async static Task<string> UploadFile(IFormFile file, string folderName)
		{
			//1. Get Located Folder Path 
			var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", folderName);

			//2. Get File Name And Make It Unique
			var fileName = $"{Guid.NewGuid()}-{Path.GetFileName(file.FileName)}";

			//3. Fet File Path
			var filePath = Path.Combine(folderPath, fileName);

			//4. 
			using var fileStream = new FileStream(filePath, FileMode.Create);

			await file.CopyToAsync(fileStream);

			return fileName;

		}

		public static void DeleteFile(string folderName, string fileName)
		{
			var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", folderName, fileName);

			if (File.Exists(filePath))
			{
				File.Delete(filePath);
			}
		}
	}
}
