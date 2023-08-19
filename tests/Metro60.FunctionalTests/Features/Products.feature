Feature: Service should serve Products

@products
Scenario: Getting all products should return 30 products
	Given I have products in the system
	When I request all products 
	Then the result should be 200
	And I should get 30 products

@products
Scenario: Get a single product should return expected product
	Given I have products in the system
	When I request a product with id: 1
	Then the result should be 200
	And I should get the product with the following:
    | Title    | Description                                 | Price | DiscountPercentage | Rating | Stock | Brand | Category    | Thumbnail                                              | 
    | iPhone 9 | An apple mobile which is nothing like apple | 549   | 12.96              | 4.69   | 94    | Apple | smartphones | https://dummyjson.com/image/i/products/1/thumbnail.jpg | 

@products
Scenario: I should be able to add a new product
    Given I have products in the system
    When I add the following product:
    | Id | Title    | Description     | Price | DiscountPercentage | Rating | Stock | Brand | Category    | Thumbnail                                              | Images                                                                                                        |
    | 31 | iPhone 4 | An apple mobile | 549   | 12.96              | 4.69   | 94    | Apple | smartphones | https://dummyjson.com/image/i/products/1/thumbnail.jpg | https://dummyjson.com/image/i/products/1/thumbnail.jpg,https://dummyjson.com/image/i/products/1/thumbnail.jpg |
    Then the result should be 201
    When I request a product with id: 31
	Then the result should be 200
	And I should get the product with the following:
    | Title    | Description     | Price | DiscountPercentage | Rating | Stock | Brand | Category    | Thumbnail                                              |
    | iPhone 4 | An apple mobile | 549   | 12.96              | 4.69   | 94    | Apple | smartphones | https://dummyjson.com/image/i/products/1/thumbnail.jpg |

@products
Scenario: I should be able to update an existing product
    Given I have products in the system
    When I update the following product:
    | Id | Title    | Description     | Price | DiscountPercentage | Rating | Stock | Brand | Category    | Thumbnail                                              | Images                                                                                                        |
    | 31 | iPhone X | An apple mobile | 300   | 12.96              | 4.69   | 94    | Apple | smartphones | https://dummyjson.com/image/i/products/1/thumbnail.jpg | https://dummyjson.com/image/i/products/1/thumbnail.jpg,https://dummyjson.com/image/i/products/1/thumbnail.jpg |
    Then the result should be 200
    When I request a product with id: 31
	Then the result should be 200
	And I should get the product with the following:
    | Title    | Description     | Price | DiscountPercentage | Rating | Stock | Brand | Category    | Thumbnail                                              |
    | iPhone X | An apple mobile | 300   | 12.96              | 4.69   | 94    | Apple | smartphones | https://dummyjson.com/image/i/products/1/thumbnail.jpg |
  
@products
Scenario: I should not be able to add a new product with existing Title and Brand
    Given I have products in the system
    When I add the following product:
    | Id | Title    | Description     | Price | DiscountPercentage | Rating | Stock | Brand | Category    | Thumbnail                                              | Images                                                                                                        |
    | 32 | iPhone 4 | An apple mobile | 549   | 12.96              | 4.69   | 94    | Apple | smartphones | https://dummyjson.com/image/i/products/1/thumbnail.jpg | https://dummyjson.com/image/i/products/1/thumbnail.jpg,https://dummyjson.com/image/i/products/1/thumbnail.jpg |
    Then the result should be 201
    When I add the following product:
    | Id | Title    | Description     | Price | DiscountPercentage | Rating | Stock | Brand | Category    | Thumbnail                                              | Images                                                                                                        |
    | 34 | iPhone 4 | An apple mobile | 549   | 12.96              | 4.69   | 94    | Apple | smartphones | https://dummyjson.com/image/i/products/1/thumbnail.jpg | https://dummyjson.com/image/i/products/1/thumbnail.jpg,https://dummyjson.com/image/i/products/1/thumbnail.jpg |
    Then the result should be 400 with the following errors:
    | key               | value                                                       |
    | ArgumentException | Product with Brand=Apple and Title=iPhone 4 already exists. |

@products
Scenario: I should not be able to add a new product with existing Id
    Given I have products in the system
    When I add the following product:
    | Id | Title    | Description     | Price | DiscountPercentage | Rating | Stock | Brand | Category    | Thumbnail                                              | Images                                                                                                        |
    | 31 | iPhone 8 | An apple mobile | 549   | 12.96              | 4.69   | 94    | Apple | smartphones | https://dummyjson.com/image/i/products/1/thumbnail.jpg | https://dummyjson.com/image/i/products/1/thumbnail.jpg,https://dummyjson.com/image/i/products/1/thumbnail.jpg |
    Then the result should be 400 with the following errors:
    | key               | value                                                       |
    | ArgumentException | An item with the same key has already been added. Key: 31   |

@products
Scenario: I should not be able to add a new product without title
    Given I have products in the system
    When I add the following product:
    | Id | Title    | Description     | Price | DiscountPercentage | Rating | Stock | Brand | Category    | Thumbnail                                              | Images                                                                                                        |
    | 33 |          | An apple mobile | 549   | 12.96              | 4.69   | 94    | Apple | smartphones | https://dummyjson.com/image/i/products/1/thumbnail.jpg | https://dummyjson.com/image/i/products/1/thumbnail.jpg,https://dummyjson.com/image/i/products/1/thumbnail.jpg |
    Then the result should be 400 with the following errors:
    | key   | value                        |
    | Title | The Title field is required. |

@products
Scenario: I should not be able to add a new product with a description of more than 100 characters
    Given I have products in the system
    When I add the following product:
    | Id | Title    | Description                                                                                                       | Price | DiscountPercentage | Rating | Stock | Brand | Category    | Thumbnail                                              | Images                                                                                                        |
    | 33 | iPhone 8 | An apple mobile.An apple mobile.An apple mobile.An apple mobile.An apple mobile.An apple mobile. An apple mobile. | 549   | 12.96              | 4.69   | 94    | Apple | smartphones | https://dummyjson.com/image/i/products/1/thumbnail.jpg | https://dummyjson.com/image/i/products/1/thumbnail.jpg,https://dummyjson.com/image/i/products/1/thumbnail.jpg |
    Then the result should be 400 with the following errors:
    | key         | value                                                                |
    | Description | The field Description must be a string with a maximum length of 100. |

@products
Scenario: I should not be able to add a new product with a price of 0
    Given I have products in the system
    When I add the following product:
    | Id | Title    | Description     | Price | DiscountPercentage | Rating | Stock | Brand | Category    | Thumbnail                                              | Images                                                                                                        |
    | 33 | iPhone 8 | An apple mobile | 0     | 12.96              | 4.69   | 94    | Apple | smartphones | https://dummyjson.com/image/i/products/1/thumbnail.jpg | https://dummyjson.com/image/i/products/1/thumbnail.jpg,https://dummyjson.com/image/i/products/1/thumbnail.jpg |
    Then the result should be 400 with the following errors:
    | key   | value                          |
    | Price | Price should be greater than 0 |

@products
Scenario: I should not be able to add a new product with a price of less than 0
    Given I have products in the system
    When I add the following product:
    | Id | Title    | Description     | Price   | DiscountPercentage | Rating | Stock | Brand | Category    | Thumbnail                                              | Images                                                                                                        |
    | 33 | iPhone 8 | An apple mobile | -10     | 12.96              | 4.69   | 94    | Apple | smartphones | https://dummyjson.com/image/i/products/1/thumbnail.jpg | https://dummyjson.com/image/i/products/1/thumbnail.jpg,https://dummyjson.com/image/i/products/1/thumbnail.jpg |
    Then the result should be 400 with the following errors:
    | key   | value                          |
    | Price | Price should be greater than 0 |

@products @authentication
Scenario: I should not be able to add a product without authentication
    Given I have products in the system
    When I add the following product without authentication:
    | Id | Title    | Description     | Price   | DiscountPercentage | Rating | Stock | Brand | Category    | Thumbnail                                              | Images                                                                                                        |
    | 33 | iPhone 8 | An apple mobile | -10     | 12.96              | 4.69   | 94    | Apple | smartphones | https://dummyjson.com/image/i/products/1/thumbnail.jpg | https://dummyjson.com/image/i/products/1/thumbnail.jpg,https://dummyjson.com/image/i/products/1/thumbnail.jpg |
    Then the result should be 401

@products @authentication
Scenario: I should not be able to update a product without authentication
    Given I have products in the system
    When I update the following product without authentication:
    | Id | Title    | Description     | Price   | DiscountPercentage | Rating | Stock | Brand | Category    | Thumbnail                                              | Images                                                                                                        |
    | 33 | iPhone 8 | An apple mobile | -10     | 12.96              | 4.69   | 94    | Apple | smartphones | https://dummyjson.com/image/i/products/1/thumbnail.jpg | https://dummyjson.com/image/i/products/1/thumbnail.jpg,https://dummyjson.com/image/i/products/1/thumbnail.jpg |
    Then the result should be 401
