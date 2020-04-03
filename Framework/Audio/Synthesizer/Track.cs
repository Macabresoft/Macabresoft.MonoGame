﻿namespace Macabre2D.Framework {

    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// A track that is played by the synthesizer.
    /// </summary>
    [DataContract]
    public sealed class Track {

        [DataMember]
        private readonly List<NoteInstance> _notes = new List<NoteInstance>();

        private float _leftChannelVolume = 0.5f;
        private float _rightChannelVolume = 0.5f;

        /// <summary>
        /// Gets the instrument.
        /// </summary>
        /// <value>The instrument.</value>
        [DataMember]
        public Instrument Instrument { get; set; } = new Instrument();

        /// <summary>
        /// Gets or sets the left channel volume.
        /// </summary>
        /// <value>The left channel volume.</value>
        [DataMember]
        public float LeftChannelVolume {
            get {
                return this._leftChannelVolume;
            }

            set {
                this._leftChannelVolume = value.Clamp(0f, 1f);
            }
        }

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>The length.</value>
        public int Length {
            get {
                return this._notes.Any() ? this._notes.Max(x => x.Beat + x.Length) : 0;
            }
        }

        /// <summary>
        /// Gets or sets the right channel volume.
        /// </summary>
        /// <value>The right channel volume.</value>
        [DataMember]
        public float RightChannelVolume {
            get {
                return this._rightChannelVolume;
            }

            set {
                this._rightChannelVolume = value.Clamp(0f, 1f);
            }
        }

        /// <summary>
        /// Adds the note.
        /// </summary>
        /// <param name="beat">The beat.</param>
        /// <param name="length">The length.</param>
        /// <param name="note">The note.</param>
        /// <param name="pitch">The pitch.</param>
        /// <returns>The added note.</returns>
        public NoteInstance AddNote(ushort beat, ushort length, Note note, Pitch pitch = Pitch.Normal) {
            var frequency = new Frequency(note, pitch);
            var newNote = new NoteInstance(beat, length, frequency);
            this._notes.Add(newNote);
            return newNote;
        }

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <param name="beat">The beat.</param>
        /// <returns>The notes for the specified beat.</returns>
        public IEnumerable<NoteInstance> GetNotes(ushort beat) {
            return this._notes.Where(x => x.Beat == beat);
        }
    }
}