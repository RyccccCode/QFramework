﻿using System;


namespace QFramework {

    public static class CollectorContextExtension {

        /// Creates a Collector.
        // TODO Obsolete since 0.42.0, April 2017
        [Obsolete("Please use context.CreateCollector(Matcher.Xyz.Added()) (or .Removed(), or .AddedOrRemoved())")]
        public static ICollector<TEntity> CreateCollector<TEntity>(
            this IContext<TEntity> context, IMatcher<TEntity> matcher, GroupEvent groupEvent) where TEntity : class, IEntity {

            return context.CreateCollector(new TriggerOnEvent<TEntity>(matcher, groupEvent));
        }

        /// Creates a Collector.
        public static ICollector<TEntity> CreateCollector<TEntity>(
            this IContext<TEntity> context, IMatcher<TEntity> matcher) where TEntity : class, IEntity {

            return context.CreateCollector(new TriggerOnEvent<TEntity>(matcher, GroupEvent.Added));
        }

        /// Creates a Collector.
        public static ICollector<TEntity> CreateCollector<TEntity>(
            this IContext<TEntity> context, params TriggerOnEvent<TEntity>[] triggers) where TEntity : class, IEntity {

            var groups = new IGroup<TEntity>[triggers.Length];
            var groupEvents = new GroupEvent[triggers.Length];

            for (int i = 0; i < triggers.Length; i++) {
                groups[i] = context.GetGroup(triggers[i].matcher);
                groupEvents[i] = triggers[i].groupEvent;
            }

            return new Collector<TEntity>(groups, groupEvents);
        }
    }
}