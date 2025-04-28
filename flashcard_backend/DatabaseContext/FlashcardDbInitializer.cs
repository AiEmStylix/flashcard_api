using flashcard_backend.Models;

namespace flashcard_backend.DatabaseContext;

public class FlashcardDbInitializer
{
    public static void SeedData(ApplicationDbContext dbContext)
    {
        if (dbContext.Decks.Any() || dbContext.FlashCards.Any())
        {
            return; //Db got seeded
        }

        var regularUser = new UserModel
        {
            Username = "learner1",
            Email = "learner1@example.com",
            PasswordHash = "hashedpassword123", // In real app, use proper password hashing
            FullName = "User",
            CreatedAt = DateTime.UtcNow,
            Role = UserRole.User
        };
        var adminUser = new UserModel
        {
            Username = "admintest1",
            Email = "admin@flashcards.com",
            PasswordHash = "adminSecurePass456", // In real app, use proper password hashing
            FullName = "Admin",
            CreatedAt = DateTime.UtcNow,
            Role = UserRole.Admin  // Admin user
        };

        var moderatorUser = new UserModel
        {
            Username = "moderatortest1",
            Email = "moderator@flashcards.com",
            PasswordHash = "modPass789", // In real app, use proper password hashing,
            FullName = "Moderator",
            CreatedAt = DateTime.UtcNow,
            Role = UserRole.Moderator  // Moderator user
        };
        
        dbContext.Users.AddRange(regularUser, adminUser, moderatorUser);
        dbContext.SaveChanges();
        
        // Create sample decks
        var spanishDeck = new DeckModel()
        {
            Name = "Spanish Vocabulary",
            Description = "Basic Spanish words and phrases for beginners",
            CreatedAt = DateTime.UtcNow,
            UserId = regularUser.Id
        };

        var mathDeck = new DeckModel()
        {
            Name = "Math Formulas",
            Description = "Common mathematical formulas for calculus and algebra",
            CreatedAt = DateTime.UtcNow,
            UserId = regularUser.Id
        };

        var programmingDeck = new DeckModel()
        {
            Name = "Programming Concepts",
            Description = "Basic programming concepts and definitions",
            CreatedAt = DateTime.UtcNow,
            UserId = regularUser.Id
        };

        // Admin's deck
        var adminDeck = new DeckModel()
        {
            Name = "System Administration",
            Description = "Key concepts in system administration",
            CreatedAt = DateTime.UtcNow,
            UserId = adminUser.Id
        };

        dbContext.Decks.AddRange(spanishDeck, mathDeck, programmingDeck, adminDeck);
        dbContext.SaveChanges();
        
        // Create sample flashcards for Spanish deck
        var spanishCards = new List<CardModel>
        {
            new CardModel
            {
                Question = "¿Cómo estás?",
                Answer = "How are you?",
                CreatedAt = DateTime.UtcNow,
                DeckId = spanishDeck.Id
            },
            new CardModel
            {
                Question = "Buenos días",
                Answer = "Good morning",
                CreatedAt = DateTime.UtcNow,
                DeckId = spanishDeck.Id
            },
            new CardModel
            {
                Question = "Gracias",
                Answer = "Thank you",
                CreatedAt = DateTime.UtcNow,
                DeckId = spanishDeck.Id
            },
            new CardModel
            {
                Question = "Por favor",
                Answer = "Please",
                CreatedAt = DateTime.UtcNow,
                DeckId = spanishDeck.Id
            },
            new CardModel
            {
                Question = "¿Dónde está el baño?",
                Answer = "Where is the bathroom?",
                CreatedAt = DateTime.UtcNow,
                DeckId = spanishDeck.Id
            }
        };

        // Create sample flashcards for Math deck
        var mathCards = new List<CardModel>
        {
            new CardModel
            {
                Question = "Pythagorean Theorem",
                Answer = "a² + b² = c²",
                CreatedAt = DateTime.UtcNow,
                DeckId = mathDeck.Id
            },
            new CardModel
            {
                Question = "Area of a circle",
                Answer = "A = πr²",
                CreatedAt = DateTime.UtcNow,
                DeckId = mathDeck.Id
            },
            new CardModel
            {
                Question = "Derivative of sin(x)",
                Answer = "cos(x)",
                CreatedAt = DateTime.UtcNow,
                DeckId = mathDeck.Id
            },
            new CardModel
            {
                Question = "Integral of e^x",
                Answer = "e^x + C",
                CreatedAt = DateTime.UtcNow,
                DeckId = mathDeck.Id
            }
        };

        // Create sample flashcards for Programming deck
        var programmingCards = new List<CardModel>
        {
            new CardModel
            {
                Question = "What is inheritance?",
                Answer = "A mechanism where a new class inherits properties and behaviors from an existing class.",
                CreatedAt = DateTime.UtcNow,
                DeckId = programmingDeck.Id
            },
            new CardModel
            {
                Question = "What is polymorphism?",
                Answer = "The ability of different objects to respond to the same method call in different ways.",
                CreatedAt = DateTime.UtcNow,
                DeckId = programmingDeck.Id
            },
            new CardModel
            {
                Question = "What is encapsulation?",
                Answer = "The bundling of data and methods that operate on that data within a single unit (class).",
                CreatedAt = DateTime.UtcNow,
                DeckId = programmingDeck.Id
            },
            new CardModel
            {
                Question = "What is a constructor?",
                Answer = "A special method used to initialize objects when they are created.",
                CreatedAt = DateTime.UtcNow,
                DeckId = programmingDeck.Id
            },
            new CardModel
            {
                Question = "What is the difference between value and reference types?",
                Answer = "Value types contain their data directly, while reference types store references to their data (addresses).",
                CreatedAt = DateTime.UtcNow,
                DeckId = programmingDeck.Id
            }
        };

        // Create sample flashcards for Admin deck
        var adminCards = new List<CardModel>
        {
            new CardModel
            {
                Question = "What is RAID?",
                Answer = "Redundant Array of Independent Disks - a data storage virtualization technology that combines multiple disk drive components into a logical unit for data redundancy and performance improvement.",
                CreatedAt = DateTime.UtcNow,
                DeckId = adminDeck.Id
            },
            new CardModel
            {
                Question = "What is a firewall?",
                Answer = "A network security system that monitors and controls incoming and outgoing network traffic based on predetermined security rules.",
                CreatedAt = DateTime.UtcNow,
                DeckId = adminDeck.Id
            },
            new CardModel
            {
                Question = "What is load balancing?",
                Answer = "The process of distributing network traffic across multiple servers to ensure no single server bears too much demand.",
                CreatedAt = DateTime.UtcNow,
                DeckId = adminDeck.Id
            }
        };
        
        dbContext.FlashCards.AddRange(spanishCards);
        dbContext.FlashCards.AddRange(mathCards);
        dbContext.FlashCards.AddRange(programmingCards);
        dbContext.FlashCards.AddRange(adminCards);
        dbContext.SaveChanges();
    }
}