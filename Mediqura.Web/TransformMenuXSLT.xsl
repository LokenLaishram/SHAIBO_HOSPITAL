<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="xml" indent="yes" encoding="utf-8"/>
	
	<!-- Replace root node name SiteMaps with SiteMapItems
       and call SiteMapListing for its children-->
	<xsl:template match="/SiteMaps">
		<SiteMapItems>
			<xsl:call-template name="SiteMapListing" />
		</SiteMapItems>
	</xsl:template>

	<!-- Allow for recursive child nodeprocessing -->
	<xsl:template name="SiteMapListing">
		<xsl:apply-templates select="SiteMap" />
	</xsl:template>

	<xsl:template match="SiteMap">
        <SiteMapItem>
			<!-- Convert SiteMap child elements to SiteMapItem attributes -->
			<xsl:attribute name="Text">
				<xsl:value-of select="Text"/>
			</xsl:attribute>
			<xsl:attribute name="ToolTip">
				<xsl:value-of select="Description"/>
			</xsl:attribute>
			<xsl:attribute name="NavigateUrl">				
				<xsl:value-of select="Url"/>
			</xsl:attribute>

			<!-- Recursively call SiteMapListing forchild menu nodes -->
			<xsl:if test="count(SiteMap) >0">
				<xsl:call-template name="SiteMapListing" />
			</xsl:if>
		</SiteMapItem>
	</xsl:template>
</xsl:stylesheet>
