/*
 * The Receive<T> methods are currently not explicitly tested, because there is, in my opinion, no
 * need to. The only thing that Receive<T> does is the creation of a Builder<T>. This is already
 * enforced via the compiler, so there is nothing to test about the types.
 * 
 * Furthermore, the ApiRequestHelper already uses Receive<T> to create the ApiRequest instances,
 * so they are already called and verified to work without throwing any kind of exception.
 *
 * If you have any suggestion for required test cases, feel free to create them and send a PR!
 */
