INSERT INTO films (
    film_id, name, original_name, release_date, description,
    country_of_production, age_rating, genre, producer, image_url, rating
) VALUES
    (1, 'The Matrix', 'The Matrix', '1999-03-31', 'A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.', 'USA', 'R', 'Action, Sci-Fi', 'Lana Wachowski, Lilly Wachowski', 'https://images.justwatch.com/poster/323481903/s166/matritsa.avif', 8.3),
    (2, 'Inception', 'Inception', '2010-07-16', 'A thief who enters the dreams of others to steal secrets from their subconscious is given a chance to have his criminal history erased.', 'USA', 'PG-13', 'Action, Adventure, Sci-Fi', 'Christopher Nolan', 'https://images.justwatch.com/poster/301841237/s166/inception.avif', 7.5),
    (3, 'Titanic', 'Titanic', '1997-12-19', 'A seventeen-year-old aristocrat falls in love with a kind but poor artist aboard the luxurious, ill-fated R.M.S. Titanic.', 'USA', 'PG-13', 'Drama, Romance', 'James Cameron', 'https://images.justwatch.com/poster/58776847/s166/titanik.avif', 7.3),
    (4, 'The Dark Knight', 'The Dark Knight', '2008-07-18', 'When the menace known as The Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham.', 'USA', 'PG-13', 'Action, Crime, Drama', 'Christopher Nolan', 'https://images.justwatch.com/poster/287403260/s166/the-dark-knight.avif', 5.6),
    (5, 'Gladiator', 'Gladiator', '2000-05-05', 'A former Roman General sets out to exact vengeance against the corrupt emperor who murdered his family and sent him into slavery.', 'USA', 'R', 'Action, Adventure, Drama', 'Ridley Scott', 'https://images.justwatch.com/poster/300717633/s166/gladiator.avif', 9.1),
    (6, 'Avatar', 'Avatar', '2009-12-18', 'A paraplegic Marine dispatched to the moon Pandora on a unique mission becomes torn between following his orders and protecting the world he feels is his home.', 'USA', 'PG-13', 'Action, Adventure, Fantasy', 'James Cameron', 'https://images.justwatch.com/poster/167060864/s166/avatar.avif', 4.2),
    (7, 'Jurassic Park', 'Jurassic Park', '1993-06-11', 'During a preview tour, a theme park suffers a major power breakdown that allows its cloned dinosaur exhibits to run amok.', 'USA', 'PG-13', 'Adventure, Sci-Fi, Thriller', 'Steven Spielberg', 'https://images.justwatch.com/poster/8722613/s166/park-iurskogo-perioda.avif', 9.9),
    (8, 'The Shawshank Redemption', 'The Shawshank Redemption', '1994-09-23', 'Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.', 'USA', 'R', 'Drama', 'Frank Darabont', 'https://images.justwatch.com/poster/125004088/s166/pobeg-iz-shoushenka.avif', 7.3),
    (9, 'Forrest Gump', 'Forrest Gump', '1994-07-06', 'The presidencies of Kennedy and Johnson, the Vietnam War, the Watergate scandal and other historical events unfold from the perspective of an Alabama man with an extraordinary life story.', 'USA', 'PG-13', 'Drama, Romance', 'Robert Zemeckis', 'https://images.justwatch.com/poster/315355524/s166/forrest-gump.avif', 4.8),
    (10, 'The Lord of the Rings: The Return of the King', 'Властелин колец: Возвращение короля', '2003-12-17', 'Gandalf and Aragorn lead the World of Men against Saurons army to draw his gaze from Frodo and Sam as they approach Mount Doom with the One Ring.', 'New Zealand, USA', 'PG-13', 'Action, Adventure, Drama', 'Peter Jackson', 'https://images.justwatch.com/poster/312778950/s166/the-lord-of-the-rings-the-return-of-the-king.avif', 8.5);


INSERT INTO actors (
    actor_id, full_name, date_of_birth
) VALUES
    (1, 'Keanu Reeves', '1964-09-02'),
    (2, 'Leonardo DiCaprio', '1974-11-11'),
    (3, 'Kate Winslet', '1975-10-05'),
    (4, 'Christian Bale', '1974-01-30'),
    (5, 'Russell Crowe', '1964-04-07'),
    (6, 'Sam Worthington', '1976-08-02'),
    (7, 'Jeff Goldblum', '1952-10-22'),
    (8, 'Tim Robbins', '1958-10-16'),
    (9, 'Tom Hanks', '1956-07-09'),
    (10, 'Elijah Wood', '1981-01-28'),
    (11, 'Christian Bale', '1974-01-30'),
    (12, 'Heath Ledger', '1979-04-04'),
    (13, 'Aaron Eckhart', '1968-03-12'),
    (14, 'Maggie Gyllenhaal', '1977-11-16'),
    (15, 'Michael Caine', '1933-03-14'),
    (16, 'Russell Crowe', '1964-04-07'),
    (17, 'Joaquin Phoenix', '1974-10-28'),
    (18, 'Connie Nielsen', '1965-07-03'),
    (19, 'Djimon Hounsou', '1964-12-24'),
    (20, 'Oliver Reed', '1938-02-13'),
    (21, 'Sam Worthington', '1976-08-02'),
    (22, 'Zoe Saldana', '1978-06-19'),
    (23, 'Sigourney Weaver', '1949-10-08'),
    (24, 'Stephen Lang', '1952-07-11'),
    (25, 'Michelle Rodriguez', '1978-07-12');



INSERT INTO providers (
    provider_id, name, duration, price
) VALUES
    (1, 'Netflix', 120, 14),
    (2, 'Amazon Prime Video', 148, 16),
    (3, 'Hulu', 195, 18),
    (4, 'Disney+', 152, 15),
    (5, 'HBO Max', 155, 13);


INSERT INTO film_actor (
    film_id, actor_id, role
) VALUES
    (1, 1, '12'), (1, 2, 'Trinity'), (1, 3, 'Morpheus'), (1, 4, 'Agent Smith'), (1, 5, 'Choi'),
    (2, 2, 'Dom Cobb'), (2, 6, 'Jack Dawson'), (2, 7, 'Rose DeWitt Bukater'), (2, 8, 'Caledon Hockley'), (2, 9, 'Molly Brown'), (2, 10, 'Thomas Andrews'),
    (3, 3, 'Rose DeWitt Bukater'), (3, 11, 'Bruce Wayne / Batman'), (3, 12, 'Joker'), (3, 13, 'Harvey Dent'), (3, 14, 'Rachel Dawes'), (3, 15, 'Alfred Pennyworth'),
    (4, 4, 'Bruce Wayne / Batman'), (4, 16, 'Maximus Decimus Meridius'), (4, 17, 'Commodus'), (4, 18, 'Lucilla'), (4, 19, 'Juba'), (4, 20, 'Proximo'),
    (5, 5, 'Maximus Decimus Meridius'), (5, 21, 'Jake Sully'), (5, 22, 'Neytiri'), (5, 23, 'Dr. Grace Augustine'), (5, 24, 'Colonel Quaritch'), (5, 25, 'Trudy Chacón'),
    (6, 6, 'Jake Sully'), (7, 7, 'Dr. Ian Malcolm'), (8, 8, 'Andy Dufresne'), (9, 9, 'Forrest Gump'), (10, 10, 'Frodo Baggins');


INSERT INTO film_provider (
    film_id, provider_id
) VALUES
    (1, 1), (2, 1), (3, 2), (4, 3), (5, 4), (6, 5), (7, 1), (8, 2), (9, 3), (10, 4);
