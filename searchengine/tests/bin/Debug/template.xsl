<?xml version="1.0" encoding="UTF-8"?>
<!-- edited with XMLSpy v2007 rel. 3 (http://www.altova.com) by usman ghani (neduet) -->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:template match="/">
		<html>
			<style>
.highlight{background:yellow;}
</style>
			<head>
				<title>
Search Results - 
<xsl:value-of select="SearchResults/Title"/>
				</title>
			</head>
			<body>
				<xsl:variable name="fontface" select="SearchResults/FontFace"/>
				<xsl:variable name="fontsize" select="SearchResults/FontSize"/>
				<font face="Arial" size="4">
					<xsl:for-each select="SearchResults/SearchResult">
						<xsl:value-of select="."/>
						<br/>
						<hr/>
						<br/>
					</xsl:for-each>
				</font>
				<xsl:variable name="hilited" select="SearchResults/HilitedContents"/>
				<a target="_self" href="file:///contents.html">
View Original Document...
</a>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
