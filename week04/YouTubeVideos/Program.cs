using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        // Primeiro vídeo
        Video firstVideo = new Video("Learning C# Classes", "Bruno Costa", 600);
        firstVideo.AddComment(new Comment("Maria", "Great explanation, thanks!"));
        firstVideo.AddComment(new Comment("John", "This really helped me understand."));
        firstVideo.AddComment(new Comment("Ana", "Could you make a video about interfaces?"));
        videos.Add(firstVideo);

        // Segundo vídeo
        Video secondVideo = new Video("Abstraction in OOP", "BYU-Idaho", 720);
        secondVideo.AddComment(new Comment("Carlos", "Very clear examples."));
        secondVideo.AddComment(new Comment("Luiza", "I finally get abstraction!"));
        secondVideo.AddComment(new Comment("Pedro", "Thanks for the great content."));
        videos.Add(secondVideo);

        // Terceiro vídeo
        Video thirdVideo = new Video("YouTube Data Project", "CSE 210 Student", 540);
        thirdVideo.AddComment(new Comment("Julia", "Nice project idea."));
        thirdVideo.AddComment(new Comment("Miguel", "I used this idea in my homework."));
        thirdVideo.AddComment(new Comment("Sofia", "Good job explaining the requirements."));
        videos.Add(thirdVideo);

        // (Opcional) Quarto vídeo - se quiser deixar ainda mais completo
        Video fourthVideo = new Video("Designing Classes in C#", "Professor Smith", 480);
        fourthVideo.AddComment(new Comment("Emily", "This makes so much sense now."));
        fourthVideo.AddComment(new Comment("Lucas", "Loved the examples with YouTube videos."));
        fourthVideo.AddComment(new Comment("Nathan", "Very helpful for CSE 210!"));
        videos.Add(fourthVideo);

        // Exibir os dados de cada vídeo e seus comentários
        foreach (Video video in videos)
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"Title: {video.GetTitle()}");
            Console.WriteLine($"Author: {video.GetAuthor()}");
            Console.WriteLine($"Length (seconds): {video.GetLengthSeconds()}");
            Console.WriteLine($"Number of comments: {video.GetNumberOfComments()}");
            Console.WriteLine("Comments:");

            foreach (Comment comment in video.GetComments())
            {
                Console.WriteLine($"- {comment.GetName()}: {comment.GetText()}");
            }

            Console.WriteLine();
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
