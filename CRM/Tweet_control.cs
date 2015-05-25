using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;

namespace CRM
{
    class Tweet_control
    {
        public Tweet_control()
        {
            TwitterCredentials.SetCredentials("3264477083-2XGKwJEJPX44IVrl85S5maKNz0Dncx38hHieiHf",
                                              "VdvawsSVhONxhu2ufD89V7qO62DBdB3Z4eEtca0aQy6h7",
                                              "AuANHRbHrUi6L0GWjqcsSuLvT",
                                              "FylAM4mFG1UfeEiOVFyInfiBxgRxYUbiTQJDqpCEaKAcHDOg15");
        }

        public static Tweetinvi.Core.Interfaces.ITweet[] getTweets(String cuenta)
        {
            return getTweets(cuenta, 100);
        } 

        public static Tweetinvi.Core.Interfaces.ITweet[] getTweets(String cuenta, int cantidad)
        {
            //Obtener el usuario
            Tweetinvi.Core.Interfaces.IUser user = Tweetinvi.User.GetUserFromScreenName(cuenta);

            //Si no existe el usuario, regresa nulo
            if (user == null)
            {
                return null;
            }

            //Generar la busqueda
            Tweetinvi.Core.Interfaces.Models.Parameters.IUserTimelineRequestParameters timelineParameter = Timeline.CreateUserTimelineRequestParameter(user);

            //Cantidad de tweets a obtener
            timelineParameter.MaximumNumberOfTweetsToRetrieve = cantidad;

            //Obtener los tweets
            var tweets = Timeline.GetUserTimeline(timelineParameter);

            if (tweets == null)
            {
                return null;
            }

            //Filtrar por los publicados por el usuario
            Tweetinvi.Core.Interfaces.ITweet[] tweetsPublicados = tweets.Where(x => x.Creator.Equals(user)).ToArray();

            return tweetsPublicados;
        }

        public static Tweetinvi.Core.Interfaces.ITweet[] buscarTweets(String busqueda)
        {
            Tweetinvi.Core.Interfaces.ITweet[] tweets = Search.SearchTweets(busqueda).ToArray();
           return tweets;
        }
    }
}
