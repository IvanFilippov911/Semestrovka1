CREATE TABLE IF NOT EXISTS public.films
(
    film_id integer NOT NULL DEFAULT nextval('films_film_id_seq'::regclass),
    name character varying(255) COLLATE pg_catalog."default" NOT NULL,
    original_name character varying(255) COLLATE pg_catalog."default" NOT NULL,
    release_date character varying(255) COLLATE pg_catalog."default" NOT NULL,
    description text COLLATE pg_catalog."default",
    country_of_production character varying(255) COLLATE pg_catalog."default",
    age_rating character varying(10) COLLATE pg_catalog."default",
    genre character varying(255) COLLATE pg_catalog."default",
    producer character varying(255) COLLATE pg_catalog."default",
    image_url text COLLATE pg_catalog."default",
    rating numeric(3,1),
    CONSTRAINT films_pkey PRIMARY KEY (film_id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.films
    OWNER to postgres;


CREATE TABLE IF NOT EXISTS public.actors
(
    actor_id integer NOT NULL DEFAULT nextval('actors_actor_id_seq'::regclass),
    full_name character varying(255) COLLATE pg_catalog."default" NOT NULL,
    date_of_birth date NOT NULL,
    CONSTRAINT actors_pkey PRIMARY KEY (actor_id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.actors
    OWNER to postgres;


CREATE TABLE IF NOT EXISTS public.providers
(
    provider_id integer NOT NULL DEFAULT nextval('providers_provider_id_seq'::regclass),
    name character varying(255) COLLATE pg_catalog."default" NOT NULL,
    duration integer,
    price integer,
    CONSTRAINT providers_pkey PRIMARY KEY (provider_id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.providers
    OWNER to postgres;


CREATE TABLE IF NOT EXISTS public.film_actor
(
    film_id integer NOT NULL,
    actor_id integer NOT NULL,
    role character varying(255) COLLATE pg_catalog."default",
    CONSTRAINT film_actor_pkey PRIMARY KEY (film_id, actor_id),
    CONSTRAINT film_actor_actor_id_fkey FOREIGN KEY (actor_id)
        REFERENCES public.actors (actor_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE,
    CONSTRAINT film_actor_film_id_fkey FOREIGN KEY (film_id)
        REFERENCES public.films (film_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.film_actor
    OWNER to postgres;


CREATE TABLE IF NOT EXISTS public.film_provider
(
    film_id integer NOT NULL,
    provider_id integer NOT NULL,
    CONSTRAINT film_provider_pkey PRIMARY KEY (film_id, provider_id),
    CONSTRAINT film_provider_film_id_fkey FOREIGN KEY (film_id)
        REFERENCES public.films (film_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE,
    CONSTRAINT film_provider_provider_id_fkey FOREIGN KEY (provider_id)
        REFERENCES public.providers (provider_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.film_provider
    OWNER to postgres;



CREATE TABLE IF NOT EXISTS public.users
(
    id integer NOT NULL DEFAULT nextval('users_id_seq'::regclass),
    name character varying(255) COLLATE pg_catalog."default" NOT NULL,
    dateofbirth character varying(255) COLLATE pg_catalog."default" NOT NULL,
    email character varying(255) COLLATE pg_catalog."default" NOT NULL,
    passwordhash character varying(255) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT users_pkey PRIMARY KEY (id),
    CONSTRAINT users_email_key UNIQUE (email)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.users
    OWNER to postgres;