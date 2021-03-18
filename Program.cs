using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Pdf;
using iText.Kernel.Pdf;

namespace GerarPDF_Seguro
{
    /// <summary>
    /// Há duas libs nesse projeto o iText e o Aspose
    /// </summary>
    class Program
    {
        //definido o caminho base do PDF para utilizar no método abaixo
        const string Path = @"D:\ALGORIX DEMANDAS E PROJETOS\GerarPDF-Seguro\resources\helper(3).pdf";
        static void Main(string[] args)
        {
            /* CÓDIGO COMENTADO ABAIXO PORQUE USA OUTRA BIBLIOTECA, ou seja são duas maneiras de criptografar o documento
             * 
             *  Maneira mais implícita de se criar um documento com senha
             */
            #region usando Aspose para criptografar
            var document = new Document(@"D:\ALGORIX DEMANDAS E PROJETOS\GerarPDF-Seguro\resources\helper(3).pdf");

            document.Encrypt("usuario", "1234", Permissions.PrintDocument, CryptoAlgorithm.AESx128);

            document.Save(@"D:\ALGORIX DEMANDAS E PROJETOS\GerarPDF-Seguro\resources\PDFSeguro.pdf");
            #endregion


            Encrypt(Path);
        }

        //Metódo criado para criptografar o PDF usando iText.Kerner.Pdf
        static void Encrypt(string file)
        {
            byte[] ownerPass = Encoding.ASCII.GetBytes("123456");
                // definindo a senha que será necessária para abrir o documento
            byte[] userPass = Encoding.ASCII.GetBytes("123");
                // recebendo o arquivo para a criptografia
            using (Stream input = new FileStream(file, FileMode.Open, FileAccess.Read,FileShare.Read))
                //definido o diretório e o nome do arquivo que ficara salvo o novo PDF protegido
            using (Stream output = new FileStream(@"D:\ALGORIX DEMANDAS E PROJETOS\TestProtected.pdf",FileMode.Create,
                FileAccess.Write, FileShare.None))
            {
                var pdfReader = new PdfReader(input);
                Console.WriteLine("Carregado arquivo {0}", file);
                WriterProperties props = new WriterProperties().SetStandardEncryption(userPass, ownerPass,
                    EncryptionConstants.ALLOW_PRINTING,
                    EncryptionConstants.ENCRYPTION_AES_128 | EncryptionConstants.DO_NOT_ENCRYPT_METADATA
                    );

                var writer = new PdfWriter(output, props);
                var pdfDocument = new PdfDocument(pdfReader, writer);
                pdfDocument.Close();
            }
        }
    }
}
