﻿namespace FakeItEasy
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using FakeItEasy.Core;

    /// <summary>
    /// Allows the developer to raise an event on a faked object.
    /// </summary>
    public static class Raise
    {
        /// <summary>
        /// Raises an event on a faked object by attatching the event handler produced by the method
        /// to the event that is to be raised.
        /// </summary>
        /// <typeparam name="TEventArgs">The type of the event args.</typeparam>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <returns>A Raise(TEventArgs)-object that exposes the eventhandler to attatch.</returns>
        [SuppressMessage("Microsoft.Security", "CA2109:ReviewVisibleEventHandlers", Justification = "Must be visible to provide the event raising syntax.")]
        public static Raise<TEventArgs> With<TEventArgs>(object sender, TEventArgs e) where TEventArgs : EventArgs
        {
            return new Raise<TEventArgs>(sender, e);
        }

        /// <summary>
        /// Raises an event on a faked object by attatching the event handler produced by the method
        /// to the event that is to be raised.
        /// </summary>
        /// <typeparam name="TEventArgs">The type of the event arguments.</typeparam>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <returns>
        /// A Raise(TEventArgs)-object that exposes the eventhandler to attatch.
        /// </returns>
        public static Raise<TEventArgs> With<TEventArgs>(TEventArgs e) where TEventArgs : EventArgs
        {
            return new Raise<TEventArgs>(null, e);
        }

        /// <summary>
        /// Raises an event with empty event arguments on a faked object by attatching the event handler produced by the method
        /// to the event that is to be raised.
        /// </summary>
        /// <returns>
        /// A Raise(TEventArgs)-object that exposes the eventhandler to attatch.
        /// </returns>
        public static Raise<EventArgs> WithEmpty()
        {
            return new Raise<EventArgs>(null, EventArgs.Empty);
        }
    }

    /// <summary>
    /// A class exposing an event handler to attatch to an event of a faked object
    /// in order to raise that event.
    /// </summary>
    /// <typeparam name="TEventArgs">The type of the event args.</typeparam>
    public class Raise<TEventArgs>
        : IEventRaiserArguments where TEventArgs : EventArgs
    {
        private readonly TEventArgs eventArguments;
        private readonly object sender;

        internal Raise(object sender, TEventArgs e)
        {
            this.sender = sender;
            this.eventArguments = e;
        }

        /// <summary>
        /// Gets a generic event handler to attatch to the event to raise.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public EventHandler<TEventArgs> Go
        {
            get { return this.Now; }
        }

        object IEventRaiserArguments.Sender
        {
            get { return this.sender; }
        }

        EventArgs IEventRaiserArguments.EventArguments
        {
            get { return this.eventArguments; }
        }

        /// <summary>
        /// Register this event handler to an event on a faked object in order to raise that event.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Event args for the event.</param>
        [SuppressMessage("Microsoft.Security", "CA2109:ReviewVisibleEventHandlers", Justification = "Must be visible to provide the event raising syntax.")]
        public void Now(object sender, TEventArgs e)
        {
            throw new NotSupportedException(ExceptionMessages.NowCalledDirectly);
        }
    }
}