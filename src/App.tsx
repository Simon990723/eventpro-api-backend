const onScroll = useCallback(() => {
  // Debounce scroll events to improve performance
  requestAnimationFrame(() => {
    // Your existing scroll logic here
    // Add proper cleanup and error handling
  });
}, []);

useEffect(() => {
  // Ensure proper cleanup of scroll listeners
  const handleScroll = (e: Event) => {
    onScroll();
  };

  window.addEventListener('scroll', handleScroll, { passive: true });

  return () => {
    window.removeEventListener('scroll', handleScroll);
  };
}, [onScroll]);
