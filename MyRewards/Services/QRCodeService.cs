using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZXing;
using ZXing.Common;
using System.Drawing.Imaging;

namespace MyRewards.Services
{
    public static class QRCodeService
    {
        private const BarcodeFormat DEFAULT_BARCODE_FORMAT = BarcodeFormat.QR_CODE;
        private static readonly ImageFormat DEFAULT_IMAGE_FORMAT = ImageFormat.Jpeg;
        private const int DEFAULT_WIDTH = 1600;
        private const int DEFAULT_HEIGHT = 1600;

        public static byte[] GenerateQRCode(string contents)
        {
            var barcodeFormat = DEFAULT_BARCODE_FORMAT;
            var imageFormat = DEFAULT_IMAGE_FORMAT;
            var width = DEFAULT_WIDTH;
            var height = DEFAULT_HEIGHT;
            var barcodeWriter = new BarcodeWriter
            {
                Format = barcodeFormat,
                Options = new EncodingOptions
                {
                    Height = height,
                    Width = width
                }
            };
            if(false)
            {
                contents = CryptoService.Encrypt(contents);
            }
            var bitmap = barcodeWriter.Write(contents);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            bitmap.Save(ms, ImageFormat.Jpeg);
            byte[] byteImage = ms.ToArray();

            return byteImage;
        }
    }
}