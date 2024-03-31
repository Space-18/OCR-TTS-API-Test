using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OCR.Application.Contracts.Services;
using OCR.Application.Exceptions;
using System.Runtime.Versioning;
using System.Speech.Synthesis;

namespace OCR.Application.Features.Files.Commands.ProcessFileToAudio
{
    internal class ProcessFileToAudioCommandHandler : IRequestHandler<ProcessFileToAudioCommand, (byte[], string)>
    {
        private readonly IUnitOfWorkS _unitOfWorkS;
        private readonly string ContainerName = GlobalConstants.AudiosLocalStorage;

        public ProcessFileToAudioCommandHandler(IUnitOfWorkS unitOfWorkS)
        {
            _unitOfWorkS = unitOfWorkS;
        }

        public async Task<(byte[], string)> Handle(ProcessFileToAudioCommand request, CancellationToken cancellationToken)
        {
            try
            {
                byte[] fileByte;

                using (var item = new MemoryStream())
                {
                    await request.File.CopyToAsync(item, cancellationToken);
                    fileByte = item.ToArray();
                }

                var textResult = _unitOfWorkS.OcrService.ProcessFileInMemory(fileByte);

                var audioPath = await ConvertToAudio(textResult);

                return audioPath;
            }
            catch (IOException)
            {
                throw new BadRequestException(GlobalConstants.IOExceptionBadRequest);
            }
        }

        private async Task<(byte[], string)> ConvertToAudio(string textResult)
        {

            string folder = Path.Combine(GlobalConstants.GetWebRootPath(), ContainerName);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string fileName = $"{Guid.NewGuid()}.wav";

            if (OperatingSystem.IsWindows())
            {
                await File.WriteAllBytesAsync($"{folder}/{fileName}", SpeechTextInByteArray(textResult));
            }

            byte[] fileByte;

            using (var ms = new MemoryStream())
            {
                await new FileStream($"{folder}/{fileName}", FileMode.Open).CopyToAsync(ms, cancellationToken: default);
                fileByte = ms.ToArray();
            }

            return (fileByte, Path.Combine(GlobalConstants.GetWebApiURL(), ContainerName, fileName).Replace("\\", "/"));
        }

        [SupportedOSPlatform("windows")]
        private static byte[] SpeechTextInByteArray(string textResult)
        {
            var synthesizer = new SpeechSynthesizer();
            var stream = new MemoryStream();
            synthesizer.SetOutputToWaveStream(stream);
            synthesizer.Speak(textResult);

            return stream.ToArray();
        }
    }
}
