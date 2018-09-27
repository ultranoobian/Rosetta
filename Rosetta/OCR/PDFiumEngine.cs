using PdfiumLight;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace OCR
{
    public class PDFiumEngine : IDisposable
    {
        PdfDocument doc;
        public readonly int PageCount = 0;
        List<PdfPage> pages = new List<PdfPage>();

        public PDFiumEngine(string path)
        {
            try
            {
                doc = new PdfDocument(path);
                PageCount = doc.PageCount();
                for (int i = 0; i < PageCount; i++)
                {
                    pages.Add(doc.GetPage(i));
                }
            }
            catch (ArgumentNullException e)
            {
                // Change into ERROR GUI state.
                throw e;
            }
        }
        public PDFiumEngine(string path, string password)
        {
            try
            {
                // Loadoad PDF at @path and attempt to decrypt with @password
                doc = new PdfDocument(path, password);
                PageCount = doc.PageCount();
                for (int i = 0; i < PageCount; i++)
                {
                    pages.Add(doc.GetPage(i));
                }
            }
            catch (ArgumentNullException e)
            {

                // Change into ERROR GUI state.
                throw e;
            }
        }

        /// <summary>
        /// Return an image based on the page number with default dimensions.
        /// </summary>
        /// <param name="page">The index of the page you wish to render</param>
        /// <returns></returns>
        public Image GetImageFromPageNumber(int page)
        {
            if (pages.Count - 1 <= page)
            {
                throw new IndexOutOfRangeException("Too far out");
            }

            return pages[page].Render(0, 500);
        }

        /// <summary>
        /// Return an image based on the page number and dimensions.
        /// </summary>
        /// <param name="page">The index of the page you wish to render</param>
        /// <param name="x">Horizontal image size in pixels</param>
        /// <param name="y">Vertical image size in pixels</param>
        /// <returns></returns>
        public Image GetImageFromPageNumber(int page, int x, int y)
        {
            if (pages.Count - 1 < page)
            {
                throw new IndexOutOfRangeException("Too far out");
            }

            return pages[page].Render(x, y);
        }

        public void Dispose()
        {
            foreach(PdfPage p in pages)
            {
                p.Dispose();
            }
            doc.Dispose();
        }
    }
}
