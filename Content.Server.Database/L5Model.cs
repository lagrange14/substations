using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Content.Server.Database;

public sealed class L5Model
{
    /// <summary>
    /// Defines an in-game book that is persisted across rounds
    /// </summary>
    [Table(name: "L5_books")]
    public class Book
    {
        public int Id { get; set; }

        /// <summary>
        /// The title of the book.
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// An optional subtitle for the book.
        /// </summary>
        public string Subtitle { get; set; } = "";

        /// <summary>
        /// A short summary for the book to be shown on the catalog listing.
        /// </summary>
        [MaxLength(512)]
        public string Summary { get; set; } = "";

        /// <summary>
        /// An optional collection that this book belongs to; usually a longer work split into volumes.
        /// </summary>
        [ForeignKey("Collection")] public int? CollectionId { get; set; }
        public BookCollection? Collection { get; set; }

        /// <summary>
        /// Since books often need to be split across multiple volumes, store which volume in the collections this book is.
        /// </summary>
        public int? Volume { get; set; }

        /// <summary>
        /// The author of the book (just a text field, not the creator, in case someone is uploading e.g: Poe's "The Raven").
        /// </summary>
        [Required, MaxLength(256)]
        public required string Author { get; set; }

        /// <summary>
        /// The player's character who clicked submit (so that books can be signed).
        /// </summary>
        [Required, ForeignKey("CreatedBy")] public Guid? CreatorUserId { get; set; }
        public Profile CreatedBy { get; set; } = default!;

        /// <summary>
        /// The librarian's character who approved the book.
        /// </summary>
        [Required, ForeignKey("ApprovedBy")] public Guid? ApprovedById { get; set; }
        public Profile ApprovedBy { get; set; } = default!;

        /// <summary>
        /// The round during which the book was approved.
        /// </summary>
        [Required, ForeignKey("RoundCreated")] public int RoundCreatedId { get; set; }
        public Round RoundCreated { get; set; } = default!;

        /// <summary>
        /// The text of the book itself.
        /// </summary>
        [Required]
        public required string Text { get; set; }

        /// <summary>
        /// A list of times the book has been checked out
        /// </summary>
        public List<BookCheckOut> CheckOuts { get; set; } = null!;
    }

    /// <summary>
    /// Defines a collection of in-game books
    /// </summary>
    [Table(name: "L5_book_collections")]
    public class BookCollection
    {
        public int Id { get; set; }

        /// <summary>
        /// The name of the collection
        /// </summary>
        [Required, MaxLength(256)]
        public required string CollectionName { get; set; }

        /// <summary>
        /// The player's character who created the collection.
        /// </summary>
        [Required, ForeignKey("CreatedBy")] public Guid? CreatedById { get; set; }
        public Profile? CreatedBy { get; set; }

        /// <summary>
        /// The books in the collection
        /// </summary>
        public List<Book> Books { get; set; } = null!;
    }

    /// <summary>
    /// Tracks times that the book was checked out
    /// </summary>
    [Table(name: "L5_book_checkouts")]
    public class BookCheckOut
    {
        public int Id { get; set; }

        /// <summary>
        /// The book being checked out.
        /// </summary>
        [Required, ForeignKey("Book")] public int BookId { get; set; }
        public required Book Book { get; set; }

        /// <summary>
        /// The character who checked out the book.
        /// </summary>
        [Required, ForeignKey("CheckedOutBy")] public Guid? CheckedOutById { get; set; }
        public Profile CheckedOutBy { get; set; } = default!;

        /// <summary>
        /// The round during which the book was checked out.
        /// </summary>
        [Required, ForeignKey("RoundCreated")] public int RoundCreatedId { get; set; }
        public Round RoundCreated { get; set; } = default!;
    }
}
